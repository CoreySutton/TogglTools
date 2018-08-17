using System;
using CoreySutton.TogglTools.TogglCore;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglConsole
{
    public static class FunctionSelector
    {
        public static TogglFunction Prompt(TogglFunctions functionCollection)
        {
            ArgUtil.NotNull(functionCollection);

            Logger.LogLine();
            Logger.LogLine("Please select a function by entering the number:");
            Logger.Log(functionCollection);

            while (true)
            {
                // Get user input
                Console.Write(">> ");
                string inputLine = Console.ReadLine();

                // Null check the input
                if (string.IsNullOrEmpty(inputLine))
                {
                    LogErrorMessage();
                    continue;
                }

                // Try parse the input to an int
                bool successfulParse = int.TryParse(inputLine, out int functionKey);
                if (!successfulParse)
                {
                    LogErrorMessage();
                    continue;
                }

                TogglFunction togglFunction = functionCollection.GetByKey(functionKey);
                if (togglFunction == null)
                {
                    LogErrorMessage();
                    continue;
                }

                Logger.LogLine($"Selected \"{togglFunction.Name}\"", ConsoleColor.Green);

                return togglFunction;
            }
        }

        private static void LogErrorMessage()
        {
            Logger.LogLine("Please enter a valid function number", ConsoleColor.Red);
        }
    }
}
