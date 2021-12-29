namespace CoreySutton.TogglTools.Core
{
    public static class PathUtil
    {
        public static string BuildFileName(string since = null, string until = null)
        {
            var fileName = "Report.csv";

            if (!string.IsNullOrEmpty(since) && !string.IsNullOrEmpty(until))
                fileName = $"Report From {since} To {until}.csv";

            else if (string.IsNullOrEmpty(since) && string.IsNullOrEmpty(until))
                fileName = $"Report From {since}.csv";

            return fileName;
        }

        public static string BuildCompletePath(string outputPath, string since = null, string until = null)
        {
            var fileName = BuildFileName(since, until);
            return $"{outputPath}{fileName}";
        }
    }
}
