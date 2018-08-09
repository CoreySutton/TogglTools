using System.Collections;
using System.Collections.Generic;
using CoreySutton.Utilities;
using Newtonsoft.Json;

namespace CoreySutton.TogglTools.TogglCore
{
    public class Workspaces : IDictionary<string, string>
    {
        public readonly Dictionary<string, string> Data;

        public Workspaces(string response = null)
        {
            Data = new Dictionary<string, string>();
            if (response != null)
            {
                Data = ProcessWorkspacesResponse(response) ?? new Dictionary<string, string>();
            }
        }

        private Dictionary<string, string> ProcessWorkspacesResponse(string response)
        {
            ArgUtil.NotNull(response);

            List<Workspace> workspaces = JsonConvert.DeserializeObject<List<Workspace>>(response);

            var workspacesDict = new Dictionary<string, string>();
            foreach (Workspace workspace in workspaces)
            {
                if (workspace.Id != null && workspace.Name != null)
                {
                    workspacesDict.Add(workspace.Id, workspace.Name);
                }
            }

            return workspacesDict.Count != 0 ? workspacesDict : null;
        }

        #region IDictionary Implementation

        public string this[string key] { get => Data[key]; set => Data[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, string>)Data).Keys;

        public ICollection<string> Values => ((IDictionary<string, string>)Data).Values;

        public int Count => Data.Count;

        public bool IsReadOnly => ((IDictionary<string, string>)Data).IsReadOnly;

        public void Add(string key, string value)
        {
            Data.Add(key, value);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            ((IDictionary<string, string>)Data).Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Data).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((IDictionary<string, string>)Data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IDictionary<string, string>)Data).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Data.Remove(key);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Data).Remove(item);
        }

        public bool TryGetValue(string key, out string value)
        {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, string>)Data).GetEnumerator();
        }

        #endregion
    }
}