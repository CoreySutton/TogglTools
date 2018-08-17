using System;

namespace CoreySutton.TogglTools.TogglConsole
{
    class DayOfWeekSelector
    {
        public static DayOfWeek Prompt()
        {
            Console.WriteLine("Please select first day of week:");
            Console.WriteLine($"(1){DayOfWeek.Sunday}");
            Console.WriteLine($"(2){DayOfWeek.Monday}");
            Console.WriteLine($"(3){DayOfWeek.Tuesday}");
            Console.WriteLine($"(4){DayOfWeek.Wednesday}");
            Console.WriteLine($"(5){DayOfWeek.Thursday}");
            Console.WriteLine($"(6){DayOfWeek.Friday}");
            Console.WriteLine($"(7){DayOfWeek.Saturday}");

            while (true)
            {
                Console.Write(">> ");
                string inputString = Console.ReadLine();

                if (string.IsNullOrEmpty(inputString))
                {
                    Console.WriteLine("Please select a day of week");
                    continue;
                }

                bool parsed = int.TryParse(inputString, out int input);
                if (!parsed)
                {
                    Console.WriteLine("Please enter a valid number");
                }

                if (input < 1 || input > 7)
                {
                    Console.WriteLine("Please enter a number between 1 and 7");
                }


                return (DayOfWeek)input - 1;
            }
        }
    }
}
