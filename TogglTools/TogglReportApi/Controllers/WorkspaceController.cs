using System.Web.Http;
using CoreySutton.TogglTools.TogglCore;

namespace CoreySutton.TogglTools.TogglReportApi
{
    public class WorkspacesController : ApiController
    {
        // GET: api/Workspaces
        public Workspaces Get()
        {
            string domain = Properties.Settings.Default.Domain;
            string workspaceEndpoint = Properties.Settings.Default.WorkspaceEndpoint;
            string reportEndpoint = Properties.Settings.Default.ReportEndpoint;
            string apiPassword = Properties.Settings.Default.DefaultApiPassword;
            string apiToken = WebUtil.GetHeader("ApiToken", Request.Headers);

            var paths = new ApiPaths(domain, workspaceEndpoint, reportEndpoint);
            var credentials = new ApiCredentials
            {
                ApiKey = apiToken,
                ApiPassword = apiPassword
            };
            var context = new ApiContext(paths, credentials);

            return new TogglApiRequest(context).GetWorkspaces();
        }
    }
}
