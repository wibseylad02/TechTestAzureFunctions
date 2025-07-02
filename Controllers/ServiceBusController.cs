using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary.Models;
using DataAccessLibrary.Commands;
using System.Net;
using DataAccessLibrary.Repositories;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TechTestAzureFunctions.Controllers
{
    [ApiController]
    [Route("api/ServiceBus")]
    //[Route("www.perch-ai")]
    public class ServiceBusController : ControllerBase
    {
        public const string ApiRoute = "www.perch-ai/api/ServiceBus";

        private readonly ILogger<ServiceBusController> _logger;
        private IWorkflowStepRepository _workflowStepRepository;

        public ServiceBusController(ILogger<ServiceBusController> logger) 
        {
            _logger = logger;
        }

        public ServiceBusController(ILogger<ServiceBusController> logger, IWorkflowStepRepository workflowStepRepository)
            : this(logger)
        {
            _workflowStepRepository = workflowStepRepository;
        }

        [HttpGet(@"{ApiRoute}/get-workflow/customerid={customerId}&Property={property}")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<(IActionResult, string?)> Get(string customerId, string property)
        {
            // DEVELOPER NOTE:
            // This code is my own, but is partially based on the Microsoft Learn module about IActionResult return values:
            // https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-8.0#iactionresult-type

            int retryCount = 0;

            // DEVELOPER NOTE:
            // Allow two retries if the service has gone to sleep.
            // You could also use the 3rd party Polly library to implement a retry policy, but I wanted to keep this simple.
            try
            {
                while (retryCount < 2)
                {
                    retryCount++;
                    _logger.LogInformation($"Attempt {retryCount} to retrieve workflow for customerId: {customerId}, Property: {property}");
                
                    var command = new WorkflowGetJsonOutputCommand
                    {
                        CustomerId = customerId,
                        Property = property
                    };

                    // Call the repository to get the workflow steps, create the instance if it is still null
                    _workflowStepRepository ??= new WorkflowStepRepository();

                    var result = await Task.FromResult(_workflowStepRepository.AddWorkflow(command));

                    if (result.Status == TaskStatus.RanToCompletion)
                    {
                        _logger.LogInformation($"Successfully returned JSON for CustomerId '{customerId}' Propetry '{property}'");
                        return (Ok(result.Result), result.Result.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Max retry attempts reached. Unable to retrieve workflow steps. Error: \r\n{ex.Message}");
                throw;
            }

            _logger.LogError($"Max {retryCount} retry attempts reached. Unable to retrieve workflow steps.");

            return (new StatusCodeResult(StatusCodes.Status503ServiceUnavailable), string.Empty);
        }


        [HttpPost(@"{ApiRoute}/feed-dataflow")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> FeedDataFlow([FromBody] string customerId, string property, string _id, int businessId)
        {
            _logger.LogInformation($"Feeding data flow for CustomerId: {customerId}, Property: {property}, ID: {_id}, BusinessId: {businessId}");

            try
            {
                var commandGetWorkflowSteps = new WorkflowStepGetCommand
                {
                    ID = _id,
                    BusinessId = 1 // Default value for BusinessId
                };

                // Call the stored procedure to get the workflow steps
                _workflowStepRepository ??= new WorkflowStepRepository();

                var workflowSteps = await _workflowStepRepository.GetWorkflowSteps(commandGetWorkflowSteps);

                if (workflowSteps == null || !workflowSteps.Result.Any())
                {
                    var message = $"No workflow steps found for ID: {_id} and BusinessId: {businessId}";

                    _logger.LogWarning(message);
                    return NotFound(message);
                }
                else
                {
                    // need to be in Ascending order by WorkflowStepId (the primary key on the table)
                    workflowSteps.Result.OrderBy(workflowSteps => workflowSteps.WorkflowStepId);
                }

                _logger.LogInformation($"Retrieved {workflowSteps.Result.Count()} workflow steps for ID: {_id} and BusinessId: {businessId}");

                foreach (var step in workflowSteps.Result)
                {
                    _logger.LogInformation($"Workflow Step: {step.StepName}, Type: {step.Type}, Property: {step.Property}");

                    var commandAdd = new WorkflowStepAddCommand
                    {
                        CustomerId = customerId,
                        Property = property,
                        Id = _id,
                        BusinessId = businessId,
                        WorkflowStepId = step.WorkflowStepId,
                        StepName = step.StepName,
                        Weight = step.Weight,
                        DelayTimeInMs = step.DelayTimeInMs
                    };

                    // DEVELOPER NOTE:
                    // Let the Serializer convert the command to JSON.
                    // I don't think there is any particular need to write any custom JSON for this data.
                    var jsonInput = JsonSerializer.Serialize(commandAdd);

                    _logger.LogInformation($"Adding workflow step with JSON: {jsonInput}");

                    // DEVELOPER NOTE:
                    // The Tech Test does not specify what to do with the JSON input.
                }

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, @"Could not feed data flow");

                throw;
            }
        }
    }
}
