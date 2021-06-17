using System;
using CoreySutton.TogglTools.Core;

namespace CoreySutton.TogglTools.Web
{
    public class ReportResponseViewModel
    {
        public string ErrorMessage { get; }
        public string HtmlSummaryTable { get; }
        public string HtmlDetailedTable { get; }

        public ReportResponseViewModel(
            Report report,
            DateTime since,
            DateTime until,
            double roundAt,
            double roundTo,
            string errorMessage = null)
        {
            ErrorMessage = errorMessage;
            HtmlSummaryTable = HtmlBuilder.SummaryTable(report, since, until, roundAt, roundTo);
            HtmlDetailedTable = HtmlBuilder.DetailedTable(report, since, until);
        }

        public ReportResponseViewModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}