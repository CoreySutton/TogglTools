using System;
using System.Web.Http;
using CoreySutton.TogglTools.Core;

namespace CoreySutton.TogglTools.Api
{
    public class ReportController : ApiController
    {
        // GET: api/Report/ThisWeek/{workspaceKey}
        public Report GetThisWeek(string workspaceKey)
        {
            string apiToken = WebUtil.GetHeader("ApiToken", Request.Headers);
            string email = WebUtil.GetHeader("Email", Request.Headers);

            var parameters = new ApiParameters(workspaceKey, DateTime.Today, DateTime.Today.AddDays(7));
            var credentials = new ApiCredentials(apiToken, email);

            return new TogglApi(credentials).GetReport(parameters);
        }
    }
}
