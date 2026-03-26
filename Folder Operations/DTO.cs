using System;
using System.Collections.Generic;
using System.Text;

namespace NeraXTools
{
    public sealed class Result
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
    }
}