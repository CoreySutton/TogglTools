using System;
using System.Collections.Generic;
using System.Text;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglConsole
{
    public class TogglFunctions
    {
        private readonly SortedDictionary<int, TogglFunction> _functions;
        private int _nextKeyValue = 1;

        public TogglFunctions()
        {
            _functions = new SortedDictionary<int, TogglFunction>();
        }

        public void Add(TogglFunction togglFunction)
        {
            Argument.IsNotNull(togglFunction);

            _functions.Add(_nextKeyValue++, togglFunction);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (KeyValuePair<int, TogglFunction> kvp in _functions)
            {
                stringBuilder.AppendLine($"{kvp.Key}): {kvp.Value.Name}");
            }

            return stringBuilder.ToString();
        }

        public TogglFunction GetByKey(int functionKey)
        {
            Argument.IsGreaterThanZero(functionKey);

            if (_functions.TryGetValue(functionKey, out TogglFunction togglFunction))
            {
                return togglFunction;
            }

            return null;
        }

        public static TogglFunctions BuildStandardCollection(DayOfWeek firstDayOfWeek)
        {
            var functionCollection = new TogglFunctions();
            functionCollection.Add(TogglFunction.ThisWeek(firstDayOfWeek));
            functionCollection.Add(TogglFunction.LastWeek(firstDayOfWeek));
            functionCollection.Add(TogglFunction.Today);
            functionCollection.Add(TogglFunction.Yesterday);

            return functionCollection;
        }
    }
}
