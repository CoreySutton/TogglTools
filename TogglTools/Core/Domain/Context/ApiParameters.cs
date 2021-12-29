using System;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Core
{
    public class ApiParameters
    {
        public string WorkspaceKey { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }

        public ApiParameters(string workspaceKey, DateTime since, DateTime? until = null)
        {
            Argument.IsNotNullOrEmpty(workspaceKey, nameof(workspaceKey));
            Argument.IsNotNull(since, nameof(since));

            WorkspaceKey = workspaceKey;
            Since = since;
            if (until.HasValue) Until = until.Value;
        }
    }
}
