using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreySutton.TogglTools.TogglCore
{
    public class RoundedDurationPerDatePerProjectReport : IDictionary<string, Dictionary<DateTime, double>>
    {
        public readonly IDictionary<string, Dictionary<DateTime, double>> Data;

        public RoundedDurationPerDatePerProjectReport(Report report, double roundAt, double roundTo)
        {
            Data = new Dictionary<string, Dictionary<DateTime, double>>();

            foreach (var reportData in report.Datas)
            {
                // Set important information from data to variables to improve code readbility
                var date = reportData.Start.Date;
                var project = reportData.Project;
                var durationInHours = TimeConversion.MillisecondsToHours(reportData.Dur);
                var roundedDuration = TimeConversion.RoundDuration(durationInHours, roundAt, roundTo);

                // Check if the project from this iteration already exists in the collection
                Data.TryGetValue(project, out Dictionary<DateTime, double> durationPerDateMap);

                // If it exists, add to it
                if (durationPerDateMap != null)
                {
                    // Check if the date from this iteration already exists in the collection
                    durationPerDateMap.TryGetValue(date, out double totalDurationForDate);

                    if (totalDurationForDate != default(double))
                    {
                        durationPerDateMap[date] = totalDurationForDate + roundedDuration;
                    }
                    else
                    {
                        durationPerDateMap.Add(date, roundedDuration);
                    }
                }
                // If it does not exist then add a new entry for this project
                else
                {
                    Data.Add(project, new Dictionary<DateTime, double>
                    {
                        {date, roundedDuration}
                    });
                }
            }
        }

        #region IDictionary Implementation

        public Dictionary<DateTime, double> this[string key] { get => Data[key]; set => Data[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, Dictionary<DateTime, double>>)Data).Keys;

        public ICollection<Dictionary<DateTime, double>> Values => ((IDictionary<string, Dictionary<DateTime, double>>)Data).Values;

        public int Count => Data.Count;

        public bool IsReadOnly => ((IDictionary<string, Dictionary<DateTime, double>>)Data).IsReadOnly;

        public void Add(string key, Dictionary<DateTime, double> value)
        {
            Data.Add(key, value);
        }

        public void Add(KeyValuePair<string, Dictionary<DateTime, double>> item)
        {
            ((IDictionary<string, Dictionary<DateTime, double>>)Data).Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(KeyValuePair<string, Dictionary<DateTime, double>> item)
        {
            return ((IDictionary<string, Dictionary<DateTime, double>>)Data).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Dictionary<DateTime, double>>[] array, int arrayIndex)
        {
            ((IDictionary<string, Dictionary<DateTime, double>>)Data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, Dictionary<DateTime, double>>> GetEnumerator()
        {
            return ((IDictionary<string, Dictionary<DateTime, double>>)Data).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Data.Remove(key);
        }

        public bool Remove(KeyValuePair<string, Dictionary<DateTime, double>> item)
        {
            return ((IDictionary<string, Dictionary<DateTime, double>>)Data).Remove(item);
        }

        public bool TryGetValue(string key, out Dictionary<DateTime, double> value)
        {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, Dictionary<DateTime, double>>)Data).GetEnumerator();
        }

        #endregion
    }
}
