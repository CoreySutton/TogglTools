﻿using System;
using System.Collections.Generic;
using System.IO;
using CoreySutton.TogglTools.Core;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Console
{
    class ReportFile
    {
        public static void Create(Report report, string outputFilePath)
        {
            Argument.IsNotNull(report);
            Argument.IsNotNull(outputFilePath);

            Logger.LogLine("\nLogging report to file");

            var output = new List<string>();
            var sortedReport = new DurationPerStoryPerProjectPerDateReport(report);

            foreach (var durationPerStoryPerProjectPerDateKvp in sortedReport)
            {
                DateTime date = durationPerStoryPerProjectPerDateKvp.Key;

                output.Add($"############################################################" +
                    $"{date:yyyy-MM-dd}" +
                    $"############################################################");

                foreach (var durationPerStoryPerProjectKvp in durationPerStoryPerProjectPerDateKvp.Value)
                {
                    var storiesForDay = GetStoriesForDay(
                        date,
                        durationPerStoryPerProjectKvp.Key,
                        durationPerStoryPerProjectKvp.Value);

                    output.AddRange(storiesForDay);
                }
            }

            File.WriteAllLines(outputFilePath, output.ToArray());
        }

        private static List<string> GetStoriesForDay(
            DateTime date,
            string project,
            Dictionary<string, double> durationPerStory)
        {
            var totalDurationForProject = 0.0;
            var output = new List<string>();
            var stories = new List<string>();

            foreach (var durationPerProjectKvp in durationPerStory)
            {
                string story = durationPerProjectKvp.Key;
                double duration = durationPerProjectKvp.Value;

                // Always round up if there is a remainder
                double remainder = duration % 0.25;
                if (remainder != 0.0)
                {
                    duration = duration - remainder + 0.25;
                }

                stories.Add($"{story} | {duration:F} hour");

                totalDurationForProject += duration;
            }

            string header = GetHeader(date, project, totalDurationForProject);
            string hash = GetBorder(header.Length);

            output.Add(hash);
            output.Add(header);
            output.Add(hash);
            output.AddRange(stories);
            output.Add("");
            output.Add("");

            return output;
        }

        private static string GetHeader(DateTime date, string project, double totalDurationForProject)
        {
            Argument.IsNotNull(date);
            Argument.IsNotNullOrEmpty(project);

            return $"{date:yyyy-MM-dd}" +
                        $" || {project}" +
                        $" || {totalDurationForProject:F} hours total";
        }

        private static string GetBorder(int length)
        {
            string border = string.Empty;
            for (var i = 0; i < length; i++)
            {
                border += "*";
            }

            return border;
        }
    }
}
