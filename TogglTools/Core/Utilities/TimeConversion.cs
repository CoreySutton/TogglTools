using System;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Core
{
    public static class TimeConversion
    {
        public static double MillisecondsToMinutes(double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes;
        }

        public static double MillisecondsToHours(double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds).TotalHours;
        }

        public static double RoundDuration(double duration, double roundAt, double roundTo)
        {
            Argument.IsGreaterThanOrEqualToZero(duration, nameof(duration));

            if (duration == 0d) return 0;

            var roundAtMinutesAsDecimal = roundAt / 60.0;
            var roundToMinutesAsDecimal = roundTo / 60.0;

            var remainder = duration % roundToMinutesAsDecimal;

            if (remainder == 0.0)
            {
                return duration;
            }

            if (remainder > roundAtMinutesAsDecimal)
            {
                return duration - remainder + roundToMinutesAsDecimal;
            }

            if (remainder <= roundAtMinutesAsDecimal)
            {
                return duration - remainder;
            }

            return duration;
        }
    }
}
