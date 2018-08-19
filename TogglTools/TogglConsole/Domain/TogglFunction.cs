using System;
using CoreySutton.TogglTools.TogglCore;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglConsole
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

        public readonly string Name;

        public readonly DateTime Since;

        public readonly DateTime Until;

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
    }
}
