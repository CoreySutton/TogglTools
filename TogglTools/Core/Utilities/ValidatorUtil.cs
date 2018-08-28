using System;
using System.Data;
using System.Text.RegularExpressions;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Core
{
    public static class ValidatorUtil
    {
        public static bool IsDatesWithinXYears(DateTime since, DateTime until, int years)
        {
            Argument.IsNotNull(since);
            Argument.IsNotNull(until);
            Argument.IsGreaterThanZero(years);

            DateTime dateLimit = since.AddYears(years);

            return until <= dateLimit;
        }

        public static bool IsDatesWithinXYears(string since, string until, int years)
        {
            Argument.IsNotNull(since);
            Argument.IsNotNull(until);
            Argument.IsGreaterThanZero(years);

            DateTime sinceDt = DateTime.Parse(since);
            DateTime untilDt = DateTime.Parse(until);

            if (sinceDt == null)
                throw new NoNullAllowedException(nameof(sinceDt));

            if (untilDt == null)
                throw new NoNullAllowedException(nameof(untilDt));

            return IsDatesWithinXYears(sinceDt, untilDt, years);
        }

        public static string ValidateDate(string date)
        {
            Argument.IsNotNull(date);

            var dateComponents = date.Split('-');
            if (dateComponents.Length == 1)
            {
                dateComponents = date.Split('/');
                if (dateComponents.Length == 1)
                {
                    return $"{date} is not invalid - delimiter is invalid";
                }
            }

            if (dateComponents.Length != 3)
            {
                return $"{date} is not invalid - could not get components";
            }

            var year = dateComponents[0];
            var month = dateComponents[1];
            var day = dateComponents[2];

            if (year.Length != 4)
            {
                return $"{date} is invalid - {year} is an invalid year";
            }

            if (month.Length != 2)
            {
                return $"{date} is invalid - {month} is an invalid month";
            }

            if (day.Length != 2)
            {
                return $"{date} is invalid (R) - {day} is an invalid month";
            }

            if (!Regex.IsMatch(year, @"^[1-2][0-9][0-9][0-9]$"))
            {
                return $"{date} is invalid (R) - {year} is an invalid year";
            }

            if (!Regex.IsMatch(month, @"^([0][0-9])|([1][0-2])$"))
            {
                return $"{date} is invalid (R) - {month} is an invalid month";
            }

            if (!Regex.IsMatch(day, @"^([0][0-9])|([1-2][0-9])|([3][0-1])$"))
            {
                return $"{date} is invalid - {day} is an invalid month";
            }

            return null;
        }

        public static DateTime? ParseDateTimeString(string startDate, string startTime = null)
        {
            Argument.IsNotNull(startDate);

            DateTime start;
            if (startTime == null)
            {
                if (DateTime.TryParse($"{startDate} 12:00 AM", out start))
                {
                    return start;
                }
            }
            else if (DateTime.TryParse(startDate + startTime, out start))
            {
                return start;
            }

            throw new Exception("Failed to parse date");
        }
    }
}
