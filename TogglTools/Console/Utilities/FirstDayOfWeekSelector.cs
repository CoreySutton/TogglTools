using System;

namespace CoreySutton.TogglTools.Console
{
    class FirstDayOfWeekSelector
    {
        public static DayOfWeek Prompt()
        {
            System.Console.WriteLine("Please select first day of week:");
            System.Console.WriteLine($"(1){DayOfWeek.Sunday}");
            System.Console.WriteLine($"(2){DayOfWeek.Monday}");
            System.Console.WriteLine($"(3){DayOfWeek.Tuesday}");
            System.Console.WriteLine($"(4){DayOfWeek.Wednesday}");
            System.Console.WriteLine($"(5){DayOfWeek.Thursday}");
            System.Console.WriteLine($"(6){DayOfWeek.Friday}");
            System.Console.WriteLine($"(7){DayOfWeek.Saturday}");

            while (true)
            {
                System.Console.Write(">> ");
                string inputString = System.Console.ReadLine();

                if (string.IsNullOrEmpty(inputString))
                {
                    System.Console.WriteLine("Please select a day of week");
                    continue;
                }

                bool parsed = int.TryParse(inputString, out int input);
                if (!parsed)
                {
                    System.Console.WriteLine("Please enter a valid number");
                }

                if (input < 1 || input > 7)
                {
                    System.Console.WriteLine("Please enter a number between 1 and 7");
                }


                return (DayOfWeek)input - 1;
            }
        }
    }
}
