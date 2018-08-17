using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglCore
{
    public class ApiPaths
    {
        public string Domain;
        public string ReportEndpoint;
        public string WorkspaceEndpoint;

        public ApiPaths(string domain, string workspaceEndpoint, string reportEndpoint)
        {
            Argument.IsNotNullOrEmpty(domain, nameof(domain));
            Argument.IsNotNullOrEmpty(workspaceEndpoint, nameof(workspaceEndpoint));
            Argument.IsNotNullOrEmpty(reportEndpoint, nameof(reportEndpoint));

            Domain = TrimTrailingCharacters(domain, "/");
            WorkspaceEndpoint = $"{domain}{workspaceEndpoint}";
            ReportEndpoint = $"{domain}{reportEndpoint}";
        }

        private string TrimTrailingCharacters(string value, string trailingCharacters)
        {
            Argument.IsNotNullOrEmpty(value, nameof(value));
            Argument.IsNotNullOrEmpty(trailingCharacters, nameof(trailingCharacters));

            return value.Substring(value.Length - trailingCharacters.Length) == trailingCharacters
                ? value.Substring(0, value.Length - trailingCharacters.Length)
                : value;
        }
    }
}
