using DataAccessLibrary.Commands;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessLibrary.Repositories
{
    public interface IWorkflowStepRepository
    {
        Task<Task<IEnumerable<WorkflowStep>>> GetWorkflowSteps(WorkflowStepGetCommand command);

        Task<ActionResult<string>> AddWorkflow(WorkflowGetJsonOutputCommand command);
    }
}