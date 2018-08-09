using System.Collections.Generic;

namespace CoreySutton.TogglTools.TogglCore
{
    public static class DictionaryUtil
    {
        public static Dictionary<TKey, List<TValue>> AddToDictionaryWithNestedList<TKey, TValue>(
            TKey key,
            TValue value,
            Dictionary<TKey, List<TValue>> dictionary)
        {
            List<TValue> nestedList;
            dictionary.TryGetValue(key, out nestedList);

            if (nestedList == null)
            {
                nestedList = new List<TValue> { value };

                dictionary.Add(key, nestedList);
            }
            else
            {
                nestedList.Add(value);
                dictionary.Remove(key);
                dictionary.Add(key, nestedList);
            }

            return dictionary;
        }
    }
}
