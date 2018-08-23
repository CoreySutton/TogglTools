using System;
using System.Web.Http;
using CoreySutton.TogglTools.TogglCore;

namespace CoreySutton.TogglTools.TogglReportApi
{
    public class ReportController : ApiController
    {
        // GET: api/Report/ThisWeek/{workspaceKey}
        public Report GetThisWeek(string workspaceKey)
        {
            string domain = Properties.Settings.Default.Domain;
            string workspaceEndpoint = Properties.Settings.Default.WorkspaceEndpoint;
            string reportEndpoint = Properties.Settings.Default.ReportEndpoint;
            string apiPassword = Properties.Settings.Default.DefaultApiPassword;
            string apiToken = WebUtil.GetHeader("ApiToken", Request.Headers);
            string email = WebUtil.GetHeader("Email", Request.Headers);

            var paths = new ApiPaths(domain, workspaceEndpoint, reportEndpoint);
            var parameters = new ApiParameters()
            {
                WorkspaceKey = workspaceKey,
                Since = DateTime.Today,
                Until = DateTime.Today.AddDays(7)
            };
            var credentials = new ApiCredentials
            {
                ApiKey = apiToken,
                ApiPassword = apiPassword,
                Email = email
            };
            var context = new ApiContext(paths, credentials, parameters);

            return new TogglApi(context).GetReport();
        }
    }
}
