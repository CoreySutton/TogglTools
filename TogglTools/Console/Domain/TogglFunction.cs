using System;
using CoreySutton.TogglTools.Core;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.Console
{
    public class TogglFunction
    {
        public static TogglFunction ThisWeek(DayOfWeek firstDayOfWeek)
        {
            return new TogglFunction("This Week", Date.GetFirstDayOfThisWeek(firstDayOfWeek));
        }

        public static TogglFunction LastWeek(DayOfWeek firstDayOfWeek)
        {
            return new TogglFunction("Last Week", Date.GetFirstDayOfThisWeek(firstDayOfWeek).AddDays(-7));
        }

        public static TogglFunction Today
        {
            get { return new TogglFunction("Today", DateTime.Today); }
        }

        public static TogglFunction Yesterday
        {
            get { return new TogglFunction("Yesterday", DateTime.Today.AddDays(-1)); }
        }

        public static TogglFunction CustomRange
        {
            get { return new TogglFunction("Custom Range"); }
        }

        public readonly string Name;

        private DateTime? since;
        public DateTime? Since
        {
            get { return since; }
            set
            {
                if (value.HasValue && Until.HasValue && !ValidatorUtil.IsDatesWithinXYears(value.Value, Until.Value, 1))
                {
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(Since)} and {nameof(Until)} cannot be more than a year apart.");
                }

                since = value;
            }
        }

        private DateTime? until;
        public DateTime? Until {
            get { return until; }
            set
            {
                if (Since.HasValue && value.HasValue && !ValidatorUtil.IsDatesWithinXYears(Since.Value, value.Value, 1))
                {
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(Since)} and {nameof(Until)} cannot be more than a year apart.");
                }

                until = value;
            }
        }

        public TogglFunction(string name, DateTime since, DateTime? until = null)
        {
            Argument.IsNotNull(name);

            if (until.HasValue && !ValidatorUtil.IsDatesWithinXYears(since, until.Value, 1))
            {
                throw new ArgumentOutOfRangeException($"{nameof(since)} and {nameof(until)} cannot be more than a year apart.");
            }

            Name = name;
            Since = since;
            Until = until ?? since.AddDays(7);
        }

        public TogglFunction(string name)
        {
            Argument.IsNotNull(name);
            Name = name;
        }
    }
}
