using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreySutton.TogglTools.Core
{
    public class StoryPerProjectPerDateReport : IDictionary<DateTime, Dictionary<string, List<Data>>>
    {
        public readonly IDictionary<DateTime, Dictionary<string, List<Data>>> Data;

        public StoryPerProjectPerDateReport(Report report)
        {
            Data = new Dictionary<DateTime, Dictionary<string, List<Data>>>();

            foreach (var reportData in report.Datas)
            {
                var date = reportData.Start.Date;
                var project = reportData.Project ?? "No Project";

                Dictionary<string, List<Data>> dataPerProjectForDate;
                Data.TryGetValue(date, out dataPerProjectForDate);

                if (dataPerProjectForDate != null)
                {
                    dataPerProjectForDate = DictionaryUtil.AddToDictionaryWithNestedList(project, reportData, dataPerProjectForDate);

                    Data[date] = dataPerProjectForDate;
                }
                else
                {
                    dataPerProjectForDate = new Dictionary<string, List<Data>> { { project, new List<Data> { reportData } } };
                    Data.Add(date, dataPerProjectForDate);
                }
            }
        }

        #region IDictionary Implementation

        public Dictionary<string, List<Data>> this[DateTime key] { get => Data[key]; set => Data[key] = value; }

        public ICollection<DateTime> Keys => ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).Keys;

        public ICollection<Dictionary<string, List<Data>>> Values => ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).Values;

        public int Count => Data.Count;

        public bool IsReadOnly => ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).IsReadOnly;

        public void Add(DateTime key, Dictionary<string, List<Data>> value)
        {
            Data.Add(key, value);
        }

        public void Add(KeyValuePair<DateTime, Dictionary<string, List<Data>>> item)
        {
            ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(KeyValuePair<DateTime, Dictionary<string, List<Data>>> item)
        {
            return ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).Contains(item);
        }

        public bool ContainsKey(DateTime key)
        {
            return Data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<DateTime, Dictionary<string, List<Data>>>[] array, int arrayIndex)
        {
            ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<DateTime, Dictionary<string, List<Data>>>> GetEnumerator()
        {
            return ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).GetEnumerator();
        }

        public bool Remove(DateTime key)
        {
            return Data.Remove(key);
        }

        public bool Remove(KeyValuePair<DateTime, Dictionary<string, List<Data>>> item)
        {
            return ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).Remove(item);
        }

        public bool TryGetValue(DateTime key, out Dictionary<string, List<Data>> value)
        {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<DateTime, Dictionary<string, List<Data>>>)Data).GetEnumerator();
        }

        #endregion
    }
}
