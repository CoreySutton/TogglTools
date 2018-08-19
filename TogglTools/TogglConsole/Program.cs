using System;
using System.Diagnostics;
using CoreySutton.TogglTools.TogglCore;

namespace CoreySutton.TogglTools.TogglConsole
{
    internal class Program
    {
        private static bool _successfulRun = false;
        private static ApiPaths _paths;
        private static ApiCredentials _credentials;

        internal static void Main(string[] args)
        {
            try
            {
                // Print application header
                PrintApplicationHeader();

                // Build context
                _paths = BuildApiPaths();
                _credentials = BuildApiCredentials();
                ApiContext apiContext = new ApiContext(_paths, _credentials);

                // Build request object
                ITogglApiRequest apiRequest = new TogglApiRequest(apiContext);

                // Get all the workspaces
                Workspaces workspaces = apiRequest.GetWorkspaces();

                // Prompt the user to select a workspace
                Workspace selectedWorkspace = WorkspaceSelector.Prompt(workspaces);

                // Prompt for first day of week
                DayOfWeek firstDayOfWeek = FirstDayOfWeekSelector.Prompt();

                // Get the function collection
                TogglFunctions functionCollection = TogglFunctions.BuildStandardCollection();

                // Select a function
                TogglFunction selectedFunction = FunctionSelector.Prompt(functionCollection);

                // Add function to context
                apiContext.Parameters = new ApiParameters()
                {
                    WorkspaceKey = selectedWorkspace.Id,
                    Since = selectedFunction.Since,
                    Until = selectedFunction.Until
                };

                // Get report from Toggl
                Report report = apiRequest.GetReport();

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
                Console.ReadLine();
            }
        }

        private static ApiPaths BuildApiPaths()
        {
            string domain = Properties.Settings.Default.Domain;
            string workspaceEndpoint = Properties.Settings.Default.WorkspaceEndpoint;
            string reportEndpoint = Properties.Settings.Default.ReportEndpoint;

            return new ApiPaths(domain, workspaceEndpoint, reportEndpoint);
        }

        private static ApiParameters BuidlApiParameters(
            string workspaceKey,
            DateTime since,
            DateTime until)
        {
            return new ApiParameters()
            {
                WorkspaceKey = workspaceKey,
                Since = since,
                Until = until
            };
        }

        private static ApiCredentials BuildApiCredentials()
        {
            string apiPassword = Properties.Settings.Default.DefaultApiPassword;
            string apiToken = Properties.Settings.Default.ApiToken;
            string email = Properties.Settings.Default.Email;

            return new ApiCredentials
            {
                ApiKey = apiToken,
                ApiPassword = apiPassword,
                Email = email
            };
        }

        private static void HandleException(string message, Exception ex)
        {
            Logger.LogLine(message, ConsoleColor.Red);
            Logger.LogLine(ex.StackTrace, ConsoleColor.Gray);
        }
    }
}
