using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreySutton.TogglTools.TogglCore
{
    public class DurationPerStoryPerProjectPerDateReport : IDictionary<DateTime, Dictionary<string, Dictionary<string, double>>>
    {
        public readonly Dictionary<DateTime, Dictionary<string, Dictionary<string, double>>> Data;

        public DurationPerStoryPerProjectPerDateReport(Report report)
        {
            foreach (var reportData in report.Datas)
            {
                var date = reportData.Start.Date;
                var project = reportData.Project;
                var story = TogglTaskParser.GetStoryName(reportData.Description);
                var durationInHours = TimeConversion.MillisecondsToHours(reportData.Dur);

                // Date
                Dictionary<string, Dictionary<string, double>> durationPerstoryPerProjectMap;
                Data.TryGetValue(date, out durationPerstoryPerProjectMap);

                if (durationPerstoryPerProjectMap != null)
                {
                    // Project
                    Dictionary<string, double> durationPerstoryMap;
                    durationPerstoryPerProjectMap.TryGetValue(project, out durationPerstoryMap);

                    if (durationPerstoryMap != null)
                    {
                        // Story
                        durationPerstoryMap.Add(story, durationInHours);
                        durationPerstoryPerProjectMap[project] = durationPerstoryMap;
                    }
                    else
                    {
                        durationPerstoryPerProjectMap.Add(
                            project,
                            new Dictionary<string, double> { { story, durationInHours } });
                    }

                    Data[date] = durationPerstoryPerProjectMap;
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
