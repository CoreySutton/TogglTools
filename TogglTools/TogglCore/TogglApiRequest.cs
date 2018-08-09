using System;
using System.Data;
using System.Net;

namespace CoreySutton.TogglTools.TogglCore
{
    /// <summary>
    /// Implement request to https://toggl.com/api/
    /// </summary>
    public class TogglApiRequest : ITogglApiRequest
    {
        private readonly ApiContext _apiContext;

        public TogglApiRequest(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public Report GetReport()
        {
            string requestUriString = $"{_apiContext.Paths.ReportEndpoint}" +
                                      $"?user_agent={_apiContext.Credentials.Email}" +
                                      $"&workspace_id={_apiContext.Parameters.WorkspaceKey}" +
                                      $"s&ince={_apiContext.Parameters.Since:yyyy-MM-dd}";

            if (_apiContext.Parameters.Until != null)
            {
                requestUriString += $"&until={_apiContext.Parameters.Until:yyyy-MM-dd}";
            }

            WebRequest request = WebRequest.Create(requestUriString);
            WebUtil.SetBasicAuthHeader(
                request,
                _apiContext.Credentials.ApiKey,
                _apiContext.Credentials.ApiPassword);

            WebResponse response = request.GetResponse();
            if (response == null)
            {
                throw new NoNullAllowedException("Response should not be null");
            }

            string responseString = WebUtil.GetResponseString(response);
            if (string.IsNullOrWhiteSpace(responseString))
            {
                throw new NoNullAllowedException("Server returned an empty response.");
            }

            Report report = Report.ProcessJsonResponse(responseString);
            if (report == null)
            {
                throw new Exception($"Could not retrieve report using request string {requestUriString}.");
            }

            return report;
        }

        public Workspaces GetWorkspaces()
        {
            WebRequest request = WebRequest.Create(_apiContext.Paths.WorkspaceEndpoint);
            WebUtil.SetBasicAuthHeader(
                request,
                _apiContext.Credentials.ApiKey,
                _apiContext.Credentials.ApiPassword);

            WebResponse response = request.GetResponse();
            if (response == null)
            {
                throw new NoNullAllowedException("Server returned an empty response");
            }

            string responseAsString = WebUtil.GetResponseString(response);
            if (string.IsNullOrEmpty(responseAsString))
            {
                throw new NoNullAllowedException("Server returned an empty response.");
            }

            return new Workspaces(responseAsString);
        }
    }
}
