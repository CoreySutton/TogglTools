﻿namespace CoreySutton.TogglTools.Web
{
    public class WorkspaceTenroxMultiViewModel
    {
        public ReportRequestFormViewModel TenroxReportFormViewModel { get; set; }
        public WorkspaceResponseViewModel WorkspaceResponseViewModel { get; set; }

        public WorkspaceTenroxMultiViewModel()
        {
            TenroxReportFormViewModel = null;
            WorkspaceResponseViewModel = null;
        }

        public WorkspaceTenroxMultiViewModel(WorkspaceResponseViewModel workspaceResponseViewModel)
        {
            TenroxReportFormViewModel = null;
            WorkspaceResponseViewModel = workspaceResponseViewModel;
        }
    }
}