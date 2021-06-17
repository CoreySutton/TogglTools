using System.Collections;
using System.Collections.Generic;
using CoreySutton.Utilities;
using Newtonsoft.Json;

namespace CoreySutton.TogglTools.Core
{
    public class Workspaces : IDictionary<int, Workspace>
    {
        public readonly IDictionary<int, Workspace> Data;

        public Workspaces(string? response = null)
        {
            Data = new Dictionary<int, Workspace>();
            if (response != null)
            {
                Data = ProcessWorkspacesResponse(response) ?? new Dictionary<int, Workspace>();
            }
        }

        private IDictionary<int, Workspace> ProcessWorkspacesResponse(string response)
        {
            Argument.IsNotNull(response);

            List<Workspace> workspaces = JsonConvert.DeserializeObject<List<Workspace>>(response);

            var workspacesDict = new Dictionary<int, Workspace>();
            int count = 1;
            foreach (Workspace workspace in workspaces)
            {
                if (workspace.Id != null && workspace.Name != null)
                {
                    workspacesDict.Add(count, workspace);
                    count++;
                }
            }

            return workspacesDict.Count != 0 ? workspacesDict : null;
        }

        #region IDictionary Implementation

        public Workspace this[int key] { get => Data[key]; set => Data[key] = value; }

        public ICollection<int> Keys => Data.Keys;

        public ICollection<Workspace> Values => Data.Values;

        public int Count => Data.Count;

        public bool IsReadOnly => Data.IsReadOnly;

        public void Add(int key, Workspace value)
        {
            Data.Add(key, value);
        }

        public void Add(KeyValuePair<int, Workspace> item)
        {
            Data.Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(KeyValuePair<int, Workspace> item)
        {
            return Data.Contains(item);
        }

        public bool ContainsKey(int key)
        {
            return Data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, Workspace>[] array, int arrayIndex)
        {
            Data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<int, Workspace>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public bool Remove(int key)
        {
            return Data.Remove(key);
        }

        public bool Remove(KeyValuePair<int, Workspace> item)
        {
            return Data.Remove(item);
        }

        public bool TryGetValue(int key, out Workspace value)
        {
            return Data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        #endregion
    }
}