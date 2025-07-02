namespace DataAccessLibrary.Models
{
    public class WorkflowStep
    {
        /// <summary>
        /// This is the unique Primary Key for the WorkflowStep.
        /// </summary>
        // NOTE  this is an auto-incrementing Identity field set on the database, but it does not (currently) form part of the API.
        // It is good practice to have a unique identifier for each database record, plus it can be used as a parameter in an HTTP GET call.
        public int WorkflowStepId { get; set; }

        /// <summary>
        /// Gets or sets the ID value, as passed in by the input message.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Buniness ID value
        /// </summary>
        public int BusinessId { get; set; } = 1; // default value

        /// <summary>
        /// Gets or sets the type of the Workflow Step (e.g. CRM).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or setd the Payload Property (e.g. email)
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the Payload Entry Type (e.g. AGENT, API).
        /// </summary>
        public string EntryType { get; set; }

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

        /// <summary>
        /// Gets or sets the date and time when the process was executed
        /// </summary>
        public DateTime ProcessDateTime { get; set; }
    }
}
