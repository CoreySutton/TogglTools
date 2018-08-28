using System;
using System.Collections.Generic;
using System.Text;
using CoreySutton.TogglTools.Core;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Web2
{
    public static class HtmlBuilder
    {
        public static string SummaryTable(
            Report report,
            DateTime since,
            DateTime until,
            double roundAt,
            double roundTo)
        {
            // Init StringBuider to store table
            var output = new StringBuilder();
            double range = Math.Ceiling((until - since).TotalDays);

            // Build table header
            output.AppendLine("<table class=\"table table - hover\">");
            output = BuildHtmlTableHeader(since, (int)range, output);
            output.AppendLine("<tbody id=\"tenrox-summary-report-table-body\" class=\"table table-striped\">");

            // Sort the tenrox report
            var durationPerDatePerProjectMap = new RoundedDurationPerDatePerProjectReport(
                report,
                roundAt,
                roundTo);

            // Iterate through each date
            foreach (var durationPerDatePerProjectKvp in durationPerDatePerProjectMap)
            {
                var project = durationPerDatePerProjectKvp.Key;
                var durationPerDateMap = durationPerDatePerProjectKvp.Value;

                output.AppendLine("<tr>");
                output.AppendLine($"<td>{project}</td>");
                
                for (int i = 0; i <= range; i++)
                {
                    DateTime date = since.AddDays(i);

                    if (durationPerDateMap.ContainsKey(date))
                    {
                        output.AppendLine($"<td>{durationPerDateMap[date]:F}</td>");
                    }
                    else
                    {
                        output.AppendLine("<td>-</td>");
                    }
                }

                output.AppendLine("</tr>");
            }

            // Print Totals
            output.AppendLine("<tr class=\"table-total-row\">");
            output.AppendLine("<td></td>");
            output.AppendLine("<td></td>");
            output.AppendLine("<td></td>");
            output.AppendLine("<td></td>");
            output.AppendLine("<td></td>");
            output.AppendLine("<td></td>");
            output.AppendLine("<td></td>");
            output.AppendLine("</tr>");

            output.AppendLine("</tbody>");
            output.AppendLine("</table>");

            return output.ToString();
        }

        public static string DetailedTable(Report report, DateTime since, DateTime until)
        {
            // Init StringBuider to store table
            var output = new StringBuilder();
            double range = Math.Ceiling((until - since).TotalDays);

            // Build table header
            output.AppendLine("<table class=\"table table - hover\">");
            output = BuildHtmlTableHeader(since, (int)range, output);
            output.AppendLine("<tbody id=\"tenrox-detailed-report-table-body\" class=\"table table-striped\">");

            // Sort the tenrox report
            var durationPerStoryPerDatePerProjectMap = new DurationPerStoryPerDatePerProjectReport(report);

            // Iterate through each date
            foreach (var durationPerStoryPerDatePerProjectKvp in durationPerStoryPerDatePerProjectMap)
            {
                var project = durationPerStoryPerDatePerProjectKvp.Key;
                var durationPerStoryPerDateKvp = durationPerStoryPerDatePerProjectKvp.Value;

                output.AppendLine("<tr>");
                output.AppendLine($"<td>{project}</td>");
                
                for (int i = 0; i <= range; i++)
                {
                    DateTime date = since.AddDays(i);

                    if (durationPerStoryPerDateKvp.ContainsKey(date))
                    {
                        var durationPerStories = durationPerStoryPerDateKvp[date];

                        var comment = new StringBuilder("<ul>");

                        foreach (var durationPerStoryKvp in durationPerStories)
                        {
                            var story = durationPerStoryKvp.Key;
                            var duration = durationPerStoryKvp.Value;

                            // Always round up if there is a remainder
                            var remainder = duration % 0.25;
                            if (remainder != 0)
                            {
                                duration = duration - remainder + 0.25;
                            }

                            comment.AppendLine($"<li>{story} | {duration:F} hour</li>");
                        }

                        comment.AppendLine("</ul>");

                        output.AppendLine($"<td>{comment.ToString()}</td>");
                    }
                    else
                    {
                        output.AppendLine("<td>-</td>");
                    }
                }

                output.AppendLine("</tr>");
            }

            output.AppendLine("</tbody>");
            output.AppendLine("</table>");

            return output.ToString();
        }

        private static StringBuilder BuildHtmlTableHeader(
            DateTime since,
            int range, 
            StringBuilder sBuilder = null)
        {
            if (sBuilder == null)
            {
                sBuilder = new StringBuilder();
            }

            sBuilder.AppendLine("<thead>");
            sBuilder.AppendLine("<tr>");
            sBuilder.AppendLine("<th>Project</th>");           

            for (int i = 0; i <= range; i++)
            {
                DateTime date = since.AddDays(i);
                sBuilder.AppendLine($"<th>{date.DayOfWeek} {date:dd/MM}</th>");
            }

            sBuilder.AppendLine("</tr>");
            sBuilder.AppendLine("</thead>");

            return sBuilder;
        }
    }
}