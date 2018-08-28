using System;
using System.Data;
using System.Net;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Core
{
    /// <summary>
    /// Implement request to https://toggl.com/api/
    /// </summary>
    public class TogglApi : ITogglApi
    {
        private readonly ApiCredentials _credentials;
        private readonly ApiPaths _paths;

        public TogglApi(ApiCredentials credentials)
        {
            Argument.IsNotNull(credentials, nameof(credentials));

            // Set global members
            _credentials = credentials;

            // Ensure ApiPassword is set
            if (string.IsNullOrEmpty(_credentials.ApiPassword))
            {
                _credentials.ApiPassword = Settings.DefaultApiPassword;
            }

            // Gather API paths
            _paths = new ApiPaths(
                Settings.TogglApiDomain,
                Settings.TogglApiWorkspaceEndpoint,
                Settings.TogglApiReportEndpoint);
        }

        public Report GetReportForThisWeek(string workspaceKey, DayOfWeek firstdayOfWeek)
        {
            Argument.IsNotNull(workspaceKey, nameof(workspaceKey));

            DateTime firstdayOfThisWeek = Date.GetFirstDayOfThisWeek(firstdayOfWeek);
            var parameters = new ApiParameters(workspaceKey, firstdayOfThisWeek);
            return GetReport(parameters);
        }

        public Report GetReportForLastWeek(string workspaceKey, DayOfWeek firstdayOfWeek)
        {
            Argument.IsNotNull(workspaceKey, nameof(workspaceKey));

            DateTime firstdayOfThisWeek = Date.GetFirstDayOfThisWeek(firstdayOfWeek);
            var parameters = new ApiParameters(workspaceKey, firstdayOfThisWeek.AddDays(-7));
            return GetReport(parameters);
        }

        public Report GetReportForToday(string workspaceKey)
        {
            Argument.IsNotNull(workspaceKey, nameof(workspaceKey));

            var parameters = new ApiParameters(workspaceKey, DateTime.Today, DateTime.Today);
            return GetReport(parameters);
        }

        public Report GetReportForYesterday(string workspaceKey)
        {
            Argument.IsNotNull(workspaceKey, nameof(workspaceKey));

            var parameters = new ApiParameters(workspaceKey, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-1));
            return GetReport(parameters);
        }

        public Report GetReportForDate(string workspaceKey, DateTime date)
        {
            Argument.IsNotNull(workspaceKey, nameof(workspaceKey));

            var parameters = new ApiParameters(workspaceKey, date);
            return GetReport(parameters);
        }

        public Report GetReport(ApiParameters parameters)
        {
            Argument.IsNotNull(parameters, nameof(parameters));
            Argument.IsNotNull(parameters.WorkspaceKey, nameof(parameters.WorkspaceKey));
            Argument.IsNotNull(parameters.Since, nameof(parameters.Since));

            string requestUriString = $"{_paths.ReportEndpoint}" +
                                      $"?user_agent={_credentials.Email}" +
                                      $"&workspace_id={parameters.WorkspaceKey}" +
                                      $"&since={parameters.Since:yyyy-MM-dd}";

            if (parameters.Until != null)
            {
                requestUriString += $"&until={parameters.Until:yyyy-MM-dd}";
            }

            WebRequest request = WebRequest.Create(requestUriString);
            WebUtil.SetBasicAuthHeader(request, _credentials.ApiKey, _credentials.ApiPassword);

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
            WebRequest request = WebRequest.Create(_paths.WorkspaceEndpoint);
            WebUtil.SetBasicAuthHeader(request, _credentials.ApiKey, _credentials.ApiPassword);

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
