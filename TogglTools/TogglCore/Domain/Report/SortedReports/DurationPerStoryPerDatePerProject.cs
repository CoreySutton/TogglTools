using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreySutton.TogglTools.TogglCore
{
    public class DurationPerStoryPerDatePerProjectReport : IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>
    {
        public readonly IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>> Data;

        public DurationPerStoryPerDatePerProjectReport(Report report)
        {
            Data = new Dictionary<string, Dictionary<DateTime, Dictionary<string, double>>>();

            foreach (var reportData in report.Datas)
            {
                var date = reportData.Start.Date;
                var project = reportData.Project;
                var story = TogglTaskParser.GetStoryName(reportData.Description);
                var durationInHours = TimeConversion.MillisecondsToHours(reportData.Dur);

                // Project
                Data.TryGetValue(
                    project,
                    out Dictionary<DateTime, Dictionary<string, double>> durationPerStoryPerDateMap);

                if (durationPerStoryPerDateMap != null)
                {
                    // Date
                    Dictionary<string, double> durationPerStoryMap;
                    durationPerStoryPerDateMap.TryGetValue(date, out durationPerStoryMap);

                    if (durationPerStoryMap != null)
                    {
                        // Story
                        durationPerStoryMap.Add(story, durationInHours);
                        durationPerStoryPerDateMap[date] = durationPerStoryMap;
                    }
                    else
                    {
                        durationPerStoryPerDateMap.Add(
                            date,
                            new Dictionary<string, double> { { story, durationInHours } });
                    }

                    Data[project] = durationPerStoryPerDateMap;
                }
                else
                {
                    Data.Add(project, new Dictionary<DateTime, Dictionary<string, double>>
                    {
                        {date, new Dictionary<string, double> {{story, durationInHours }}}
                    });
                }
            }
        }

        #region IDictionary Implmentation

        public Dictionary<DateTime, Dictionary<string, double>> this[string key] { get => Data[key]; set => Data[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).Keys;

        public ICollection<Dictionary<DateTime, Dictionary<string, double>>> Values => ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).Values;

        public int Count => Data.Count;

        public bool IsReadOnly => ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).IsReadOnly;

        public void Add(string key, Dictionary<DateTime, Dictionary<string, double>> value)
        {
            Data.Add(key, value);
        }

        public void Add(KeyValuePair<string, Dictionary<DateTime, Dictionary<string, double>>> item)
        {
            ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(KeyValuePair<string, Dictionary<DateTime, Dictionary<string, double>>> item)
        {
            return ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Dictionary<DateTime, Dictionary<string, double>>>[] array, int arrayIndex)
        {
            ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, Dictionary<DateTime, Dictionary<string, double>>>> GetEnumerator()
        {
            return ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Data.Remove(key);
        }

        public bool Remove(KeyValuePair<string, Dictionary<DateTime, Dictionary<string, double>>> item)
        {
            return ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).Remove(item);
        }

        public bool TryGetValue(string key, out Dictionary<DateTime, Dictionary<string, double>> value)
        {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, Dictionary<DateTime, Dictionary<string, double>>>)Data).GetEnumerator();
        }

        #endregion
    }
}
