using System;

namespace TennisAssociation.Models
{
    /// <summary>
    /// Class for handling errors.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
