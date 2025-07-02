namespace DataAccessLibrary.Commands
{
    public class WorkflowStepAddCommand
    {
        public string CustomerId { get; set; }

        public string Property { get; set; }
        public string Id { get; set; }
        public int BusinessId { get; set; }

        // WorkflowStep properties returned from the stored procedure

        /// <summary>
        /// This is the unique Primary Key for the WorkflowStep.
        /// </summary>
        public int WorkflowStepId { get; set; }

        /// <summary>
        /// Gets or sets the Step name (e.g. Validate agent).
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// Gets or sets the Weight of the Workflow Step.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets the Delay Time in milliseconds for the Workflow Step.
        /// </summary>
        public int DelayTimeInMs { get; set; }
    }
}
