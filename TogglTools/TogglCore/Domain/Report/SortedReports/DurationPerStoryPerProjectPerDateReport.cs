using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreySutton.TogglTools.TogglCore
{
    public class DurationPerStoryPerProjectPerDateReport : IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>
    {
        public readonly IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>> Data;

        public DurationPerStoryPerProjectPerDateReport(Report report)
        {
            Data = new Dictionary<DateTime, Dictionary<string, Dictionary<string, double>>>();

            foreach (var reportData in report.Datas)
            {
                var date = reportData.Start.Date;
                var project = reportData.Project ?? "No Project";
                var story = string.IsNullOrEmpty(reportData.Description)
                    ? "No Description"
                    : TogglTaskParser.GetStoryName(reportData.Description);
                var durationInHours = TimeConversion.MillisecondsToHours(reportData.Dur);

                // Date
                Data.TryGetValue(
                    date,
                    out Dictionary<string, Dictionary<string, double>> durationPerStoryPerProjectMap);

                if (durationPerStoryPerProjectMap != null)
                {
                    // Project
                    durationPerStoryPerProjectMap.TryGetValue(
                        project,
                        out Dictionary<string, double> durationPerStoryMap);

                    if (durationPerStoryMap != null)
                    {
                        // Story
                        if (durationPerStoryMap.ContainsKey(story))
                        {
                            durationPerStoryMap[story] = durationPerStoryMap[story] + durationInHours;
                        }
                        else
                        {
                            durationPerStoryMap.Add(story, durationInHours);
                        }

                        durationPerStoryPerProjectMap[project] = durationPerStoryMap;
                    }
                    else
                    {
                        durationPerStoryPerProjectMap.Add(
                            project,
                            new Dictionary<string, double> { { story, durationInHours } });
                    }

                    Data[date] = durationPerStoryPerProjectMap;
                }
                else
                {
                    Data.Add(date, new Dictionary<string, Dictionary<string, double>>
                        {
                            {project, new Dictionary<string, double> {{story, durationInHours }}}
                        });
                }
            }
        }

        #region IDictionary

        public Dictionary<string, Dictionary<string, double>> this[DateTime key] { get => Data[key]; set => Data[key] = value; }

        public ICollection<DateTime> Keys => ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).Keys;

        public ICollection<Dictionary<string, Dictionary<string, double>>> Values => ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).Values;

        public int Count => Data.Count;

        public bool IsReadOnly => ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).IsReadOnly;

        public void Add(DateTime key, Dictionary<string, Dictionary<string, double>> value)
        {
            Data.Add(key, value);
        }

        public void Add(KeyValuePair<DateTime, Dictionary<string, Dictionary<string, double>>> item)
        {
            ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(KeyValuePair<DateTime, Dictionary<string, Dictionary<string, double>>> item)
        {
            return ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).Contains(item);
        }

        public bool ContainsKey(DateTime key)
        {
            return Data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DateTime, Dictionary<string, Dictionary<string, double>>>[] array, int arrayIndex)
        {
            ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<DateTime, Dictionary<string, Dictionary<string, double>>>> GetEnumerator()
        {
            return ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).GetEnumerator();
        }

        public bool Remove(DateTime key)
        {
            return Data.Remove(key);
        }

        public bool Remove(KeyValuePair<DateTime, Dictionary<string, Dictionary<string, double>>> item)
        {
            return ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).Remove(item);
        }

        public bool TryGetValue(DateTime key, out Dictionary<string, Dictionary<string, double>> value)
        {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>)Data).GetEnumerator();
        }

        #endregion
    }
}
