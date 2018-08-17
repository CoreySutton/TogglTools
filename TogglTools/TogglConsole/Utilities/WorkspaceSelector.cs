using System;
using System.Collections.Generic;
using System.Linq;
using CoreySutton.TogglTools.TogglCore;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglConsole
{
    public static class WorkspaceSelector
    {
        public static Workspace Prompt(Workspaces workspaces)
        {
            Argument.IsNotNull(workspaces, nameof(workspaces));

            if (workspaces.Count == 1)
            {
                Logger.LogLine("\nFound 1 workspace.");
                Logger.LogLine(
                    $"Automatically selecting {workspaces.First().Value.Name}" +
                    $" (Key: {workspaces.First().Value.Id})",
                    ConsoleColor.Green);

                return workspaces.First().Value;
            }

            Logger.LogLine($"Found {workspaces.Count} workspaces.");

            PrintWorkspaceOptions(workspaces);

            return PromptToEnterWorkspaceKey(workspaces);
        }

        private static void PrintWorkspaceOptions(Workspaces workspaces)
        {
            Argument.IsNotNull(workspaces, nameof(workspaces));

            foreach (KeyValuePair<int, Workspace> workspaceKvp in workspaces)
            {
                Logger.LogLine(
                    $"\t{workspaceKvp.Key}) {workspaceKvp.Value.Name} " +
                    $"(Key: {workspaceKvp.Value.Id})");
            }
        }

        private static Workspace PromptToEnterWorkspaceKey(Workspaces workspaces)
        {
            Argument.IsNotNull(workspaces, nameof(workspaces));

            while (true)
            {
                Logger.LogLine($"Please select a workspace (1-{workspaces.Count}):");

                Console.Write(">> ");
                string indexInput = Console.ReadLine();

                if (string.IsNullOrEmpty(indexInput))
                {
                    Console.WriteLine("You must enter a workspace number");
                    continue;
                }

                bool parsed = int.TryParse(indexInput, out int index);
                if (!parsed)
                {
                    Console.WriteLine("You must enter a number");
                    continue;
                }

                if (index < 1 || index > workspaces.Count)
                {
                    Console.WriteLine($"You must enter a number between 1 and {workspaces.Count}");
                    continue;
                }

                workspaces.TryGetValue(index, out Workspace selectedWorkspace);
                if (selectedWorkspace == null)
                {
                    Logger.LogLine("Failed to find workspace");
                    continue;
                }

                return selectedWorkspace;
            }
        }
    }
}
