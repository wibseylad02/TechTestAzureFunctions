using Dapper;
using DataAccessLibrary.Commands;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccessLibrary.Repositories
{
    // Derive from an interface to allow for dependency injection and mocking in any potential unit tests.
    public class WorkflowStepRepository : IWorkflowStepRepository
    {
        // NOTE - This string was generated with help from Stack Overflow
        // https://stackoverflow.com/questions/10479763/how-to-get-the-connection-string-from-a-database
        private const string DefaultConnectionString = @"Data Source=DESKTOP-7CLD65E\\SQLEXPRESS;Initial Catalog=PerchTest;Integrated Security=SSPI;";

        private string _connectionString;
        private readonly IConfiguration _configuration;


        public WorkflowStepRepository()
        {
            _connectionString = DefaultConnectionString; 
        }

        // DEVELOPER NOTE:
        // This constructor can be used when the connection string is stored externally, e.g. in the appsettings.json file.
        // This is useful for dependency injection in ASP.NET Core applications, or for injecting a Mock Iconfiguration
        // instance for unit tests.
        public WorkflowStepRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            // change the name of "DefaultConnection" to match your connectionString entry in appsettings.json.
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Retrieve Workflow steps from the database with the specified criteria
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Task<IEnumerable<WorkflowStep>>> GetWorkflowSteps(WorkflowStepGetCommand command)
        {
            // DEVELOPER NOTE: This was how I called stored procedures in my previous job with Dapper.
            const string procedureName = "dbo.GetNetWorkflowStep";

            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();

                // To make life easier with Dapper, ensure that the property names in the command match exactly the parameter names in the stored procedure.
                parameters.Add("@ID", command.ID, DbType.String);
                parameters.Add("@BusinessId", command.BusinessId, DbType.Int32);

                var results = await connection.QueryAsync<WorkflowStep>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                // Sort the results by WorkflowStepId in Ascending order
                IEnumerable<WorkflowStep> sortedResults = results.OrderBy(results => results.WorkflowStepId);

                return Task.FromResult(sortedResults);
            }
        }

        // This just generates a 200 OK response with a JSON message, as per Appendix B.
        public async Task<ActionResult<string>> AddWorkflow(WorkflowGetJsonOutputCommand command)
        {
            var customerId = command.CustomerId;
            var property = command.Property;

            // use the date format "yyyy-MM-ddTHH:mm:ss.fffZ" to match the expected output format in the JSON.
            var date1 = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var date2 = DateTime.UtcNow.AddMinutes(3).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var outputJson = @"{
                ""_id"": ""682372e7c447d98636e099b4"",
                ""operation"": ""insert"",
                ""payload"": {
                    ""status"": ""success"",
                    ""_collection"": ""crm"",
                    ""action"": ""ADD"",
                    ""date"": ""{date1}"",
                    ""entryType"": ""API""
                    },
                ""date"": ""{date2}""
                }";

            //var outputJson = @"{
            //    ""_id"": ""682372e7c447d98636e099b4"",
            //    ""operation"": ""insert"",
            //    ""payload"": {
            //        ""status"": ""success"",
            //        ""_collection"": ""crm"",
            //        ""action"": ""ADD"",
            //        ""date"": ""2025-07-01T15:24:16.328Z"",
            //        ""entryType"": ""API""
            //        },
            //    ""date"": ""2025-05-13T16:27:19.555Z""
            //    }";

            ActionResult<string> result = new OkObjectResult(outputJson);

            return await Task.FromResult(result);
        }
    }
}
