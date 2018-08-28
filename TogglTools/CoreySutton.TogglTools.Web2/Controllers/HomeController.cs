using System;
using System.Data;
using System.Web.Mvc;
using CoreySutton.TogglTools.Core;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Web2
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new WorkspaceRequestFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetWorkspacesFormAction(WorkspaceRequestFormViewModel workspaceFormModel)
        {
            Argument.IsNotNull(workspaceFormModel);
            Argument.IsNotNull(workspaceFormModel.Email);
            Argument.IsNotNull(workspaceFormModel.ApiToken);

            WorkspaceResponseViewModel workspaceResponseModel = null;

            if (ModelState.IsValid)
            {
                try
                {
                    // Build a TogglContext object
                    string email = workspaceFormModel.Email;
                    string apiToken = workspaceFormModel.ApiToken;

                    var credentials = new ApiCredentials(apiToken, email);

                    // Retrieve workspaces
                    Workspaces workspaces = new TogglApi(credentials).GetWorkspaces();

                    workspaceResponseModel = new WorkspaceResponseViewModel(
                        email: email,
                        apiToken: apiToken,
                        workspaces: workspaces);
                }
                catch (Exception ex)
                {
                    workspaceResponseModel = new WorkspaceResponseViewModel(ex.Message);
                }
            }

            return PartialView("_WorkspaceResponse", workspaceResponseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult GetReportFormAction(ReportRequestFormViewModel reportFormViewModel)
        {
            Argument.IsNotNull(reportFormViewModel);

            if (reportFormViewModel.Email == null)
                throw new NoNullAllowedException("Email cannot be null");

            if (reportFormViewModel.ApiToken == null)
                throw new NoNullAllowedException("API Token cannot be null");

            if (reportFormViewModel.WorkspaceId == null)
                throw new NoNullAllowedException("Workspace ID cannot be null");

            if (reportFormViewModel.RoundAt == 0.0)
                throw new NoNullAllowedException("Round at cannot be zero");

            if (reportFormViewModel.RoundTo == 0.0)
                throw new NoNullAllowedException("Round to cannot be zero");

            if (reportFormViewModel.RangeStart == DateTime.MinValue)
                throw new NoNullAllowedException("Round to cannot be zero");

            if (reportFormViewModel.RangeEnd == DateTime.MinValue)
                throw new NoNullAllowedException("Round to cannot be zero");

            if (reportFormViewModel.RangeEnd < reportFormViewModel.RangeStart)
                throw new NoNullAllowedException("Range end must be on or after range start");

            ReportResponseViewModel reportResponseViewModel = null;

            if (ModelState.IsValid)
            {
                try
                {
                    var email = reportFormViewModel.Email;
                    var apiToken = reportFormViewModel.ApiToken;
                    ApiCredentials credentials = new ApiCredentials(apiToken, email);

                    var workspaceId = reportFormViewModel.WorkspaceId;
                    var rangeStart = reportFormViewModel.RangeStart;
                    var rangeEnd = reportFormViewModel.RangeEnd;
                    ApiParameters parameters = new ApiParameters(workspaceId, rangeStart, rangeEnd);

                    var report = new TogglApi(credentials).GetReport(parameters);

                    var roundAt = reportFormViewModel.RoundAt;
                    var roundTo = reportFormViewModel.RoundTo;
                    reportResponseViewModel = new ReportResponseViewModel(report, rangeStart, rangeEnd, roundAt, roundTo);
                }
                catch (Exception ex)
                {
                    reportResponseViewModel = new ReportResponseViewModel(ex.Message + ex.StackTrace);
                }
            }

            return PartialView("_ReportResponse", reportResponseViewModel);
        }
    }
}