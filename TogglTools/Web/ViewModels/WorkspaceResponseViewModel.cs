using System.Collections.Generic;
using CoreySutton.TogglTools.Core;

namespace CoreySutton.TogglTools.Web
{
    public class WorkspaceResponseViewModel
    {
        public Workspaces Workspaces { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string Email { get; set; }
        public string ApiToken { get; set; }

        public WorkspaceResponseViewModel(Workspaces workspaces)
        {
            Workspaces = workspaces;
        }

        public WorkspaceResponseViewModel(string email, string apiToken, Workspaces workspaces)
        {
            Email = email;
            ApiToken = apiToken;
            Workspaces = workspaces;
        }

        public WorkspaceResponseViewModel(string errorMessage)
        {
            Workspaces = new Workspaces();
            ErrorMessages = new List<string> { errorMessage };
        }
    }
}