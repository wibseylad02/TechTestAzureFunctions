namespace DataAccessLibrary.Commands
{
    public class WorkflowStepGetCommand
    {
        public string ID { get; set; }
        public int BusinessId { get; set; } = 1; // default value
    }
}
