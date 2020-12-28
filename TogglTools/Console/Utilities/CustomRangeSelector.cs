using System;

namespace CoreySutton.TogglTools.Console
{
    class CustomRangeSelector
    {
        public static DateTime PromptForSince()
        {
            return PromptForDateOnly("Enter the custom range start date in the format dd/mm/yyyy:");
        }

        public static DateTime PromptForCustomUntil()
        {
            return PromptForDateOnly("Enter the custom range end date in the format dd/mm/yyyy:");
        }

        public static DateTime? PromptForUntil(DateTime since)
        {
            System.Console.WriteLine("Choose a preset end date or enter your own:");
            System.Console.WriteLine($"(1) Start day only");
            System.Console.WriteLine($"(2) Start day and next day");
            System.Console.WriteLine($"(3) Next 5 days (including start date)");
            System.Console.WriteLine($"(4) Next 7 days (including start date)");
            System.Console.WriteLine($"(5) Next 14 days (including start date)");
            System.Console.WriteLine($"(6) Custom end date");
            System.Console.WriteLine($"(7) No end date");

            while (true)
            {
                System.Console.Write(">> ");
                string inputString = System.Console.ReadLine();

                if (string.IsNullOrEmpty(inputString))
                {
                    System.Console.WriteLine("Please select an option");
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

                switch (input)
                {
                    case 1:
                        return since;
                    case 2:
                        return since.AddDays(1);
                    case 3:
                        return since.AddDays(4);
                    case 4:
                        return since.AddDays(6);
                    case 5:
                        return since.AddDays(13);
                    case 6:
                        return PromptForCustomUntil();
                    case 7:
                        return null;
                    default:
                        System.Console.WriteLine("Please enter a number between 1 and 7");
                        break;
                }
            }
        }

        public static DateTime PromptForDateOnly(string promptMessage)
        {
            System.Console.WriteLine(promptMessage);

            while (true)
            {
                System.Console.Write(">> ");
                string inputString = System.Console.ReadLine();

                if (string.IsNullOrEmpty(inputString))
                {
                    System.Console.WriteLine("Please enter a value");
                    continue;
                }

                bool parsed = DateTime.TryParse(inputString, out DateTime until);
                if (!parsed)
                {
                    System.Console.WriteLine("Please enter a valid date in the format dd/mm/yyyy");
                }

                return until.Date;
            }
        }
    }
}
