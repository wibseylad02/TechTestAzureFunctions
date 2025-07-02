using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// From https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide?tabs=ihostapplicationbuilder%2Cwindows#methods-recognized-as-functions
builder.Services.Configure<JsonSerializerOptions>(jsonSerializerOptions =>
{
    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    jsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

    // allow case-insensitive property names in JSON
    jsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddRoutingCore();

builder.Build().Run();
