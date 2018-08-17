using System;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglCore
{
    public class Logger
    {
        private const ConsoleColor DefaultConsoleColor = ConsoleColor.Gray;

        public static void LogLine()
        {
            Console.WriteLine();

            // Todo write to flat text file
        }

        public static void LogLine(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            if (message == null)
            {
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ForegroundColor = DefaultConsoleColor;

                // Todo write to flat text file
            }
        }

        public static void Log(object message, ConsoleColor color = ConsoleColor.Gray)
        {
            Argument.IsNotNull(message);

            Console.ForegroundColor = color;
            Console.Write(message.ToString());
            Console.ForegroundColor = DefaultConsoleColor;

            // Todo write to flat text file
        }
    }
}
