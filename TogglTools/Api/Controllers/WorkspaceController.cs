using System.Web.Http;
using CoreySutton.TogglTools.Core;

namespace CoreySutton.TogglTools.Api
{
    public class WorkspacesController : ApiController
    {
        // GET: api/Workspaces
        public Workspaces Get()
        {
            string apiToken = WebUtil.GetHeader("ApiToken", Request.Headers);
            string email = WebUtil.GetHeader("Email", Request.Headers);

            var credentials = new ApiCredentials(apiToken, email);

            return new TogglApi(credentials).GetWorkspaces();
        }
    }
}
