using System;
using System.Diagnostics;
using CoreySutton.TogglTools.Core;

namespace CoreySutton.TogglTools.Console
{
    internal class Program
    {
        private static bool _successfulRun = false;

        internal static void Main(string[] args)
        {
            try
            {
                // Print application header
                PrintApplicationHeader();

                // Build credentials
                ApiCredentials credentials = new ApiCredentials(
                    Properties.Settings.Default.ApiToken, 
                    Properties.Settings.Default.Email);

                // Build request object
                TogglApi apiRequest = new TogglApi(credentials);

                // Get all the workspaces
                Workspaces workspaces = apiRequest.GetWorkspaces();

                // Prompt the user to select a workspace
                Workspace selectedWorkspace = WorkspaceSelector.Prompt(workspaces);

                // Prompt for first day of week
                DayOfWeek firstDayOfWeek = FirstDayOfWeekSelector.Prompt();

                // Get the function collection
                TogglFunctions functionCollection = TogglFunctions.BuildStandardCollection(firstDayOfWeek);

                // Select a function
                TogglFunction selectedFunction = FunctionSelector.Prompt(functionCollection);

                // Build parameters
                var parameters = new ApiParameters(
                    selectedWorkspace.Id, 
                    selectedFunction.Since, 
                    selectedFunction.Until);

                // Get report from Toggl
                Report report = apiRequest.GetReport(parameters);

                // Output report to file
                ReportFile.Create(report, Properties.Settings.Default.OutputPath);

                // Open the report
                Process.Start(Properties.Settings.Default.OutputPath);

                _successfulRun = true;
            }
            catch (ArgumentNullException ex)
            {
                HandleException($"An error occurred: Argument should not be null {ex.Message}", ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                HandleException($"An error occurred: Argument is out of range {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                HandleException($"An error occurred: {ex.Message}", ex);
            }

            CloseApplication();
        }

        private static void PrintApplicationHeader()
        {
            Logger.LogLine("************************");
            Logger.LogLine("***Toggle Integration***");
            Logger.LogLine("************************");
        }

        private static void CloseApplication()
        {
            if (_successfulRun == false)
            {
                Logger.LogLine("Execution complete. Press <enter> to close application...");
                System.Console.ReadLine();
            }
        }

        private static void HandleException(string message, Exception ex)
        {
            Logger.LogLine(message, ConsoleColor.Red);
            Logger.LogLine(ex.StackTrace, ConsoleColor.Gray);
        }
    }
}
