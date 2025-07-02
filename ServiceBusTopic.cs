using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TechTestAzureFunctions;

/*
 * DEVELOPER NOTE:
 * This class is an Azure Function that serves as a simple HTTP trigger.  
 * As I was previously unfamiliar with Azure Functions, it is based on the Microsoft Learn module: 
 * "Quickstart: Create your first C# function in Azure using Visual Studio".
 * https://learn.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio
 *  
 */
public class ServiceBusTopic
{
    private readonly ILogger<ServiceBusTopic> _logger;

    public ServiceBusTopic(ILogger<ServiceBusTopic> logger)
    {
        _logger = logger;
    }

    [Function("ServiceBusTopic")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!  Now the HTTP GET and POST calls need to be plumbed in.");
    }
}