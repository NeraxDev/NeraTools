namespace NeraTools
{
    public sealed class ProcessRunResult
    {
        /// <summary>
        /// True if operation succeeded for all items
        /// </summary>
        public bool Success { get; init; }

        /// <summary>
        /// Number of successfully processed items
        /// </summary>
        public int SuccessCount { get; init; }

        /// <summary>
        /// Number of failed processed items
        /// </summary>
        public int FailedCount { get; init; }

        /// <summary>
        /// Gets or sets the list of process identifiers (PIDs) associated with the current context.
        /// </summary>
        public List<int> PIDs { get; set; }
    }
}