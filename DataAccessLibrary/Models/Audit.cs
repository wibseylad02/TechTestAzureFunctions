namespace DataAccessLibrary.Models
{
    /// <summary>
    /// Represents an Audit record for the creation of a WorkfloStep.
    /// </summary>
    public class Audit
    {
        /// <summary>
        /// Gets or sets the ID value, as passed in by the input message.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Buniness ID value
        /// </summary>
        public int BusinessId { get; set; } = 1; // default value

        /// <summary>
        /// The data and time when the audit record was created.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
    }
}
