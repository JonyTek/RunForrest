using System;
using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal static class TaskCollection
    {
        private static readonly Dictionary<string, Task> Tasks = new Dictionary<string, Task>();

        private static void Insert(string alias, Task task)
        {
            var lowerAlias = alias.ToLower();

            if (Tasks.ContainsKey(lowerAlias))
            {
                throw new InvalidOperationException(
                    string.Format("Task '{0}' with same alias already exists", alias));
            }

            Tasks.Add(lowerAlias, task);
        }

        internal static void Initialise<T>(RunForrestConfiguration config)
        {
            var assemblies = new[] {typeof (T).Assembly}.ToList();

            if (config.AdditionalAssembliesToScanForTasks != null)
            {
                assemblies.AddRange(config.AdditionalAssembliesToScanForTasks);
            }

            foreach (var task in assemblies.SelectMany(x => x.ScanForTasks()))
            {
                Insert(task.Alias, task);
            }
        }

        internal static Task Select(string alias)
        {
            var lowerAlias = alias.ToLower();

            if (!Tasks.ContainsKey(lowerAlias))
            {
                throw new KeyNotFoundException(
                    string.Format("No task found by the alias of '{0}'", alias));
            }

            return Tasks[lowerAlias];
        }

        internal static bool ContainsAlias(string alias)
        {
            return Tasks.ContainsKey(alias.ToLower());
        }

        internal static void PrintList()
        {
            foreach (var task in Tasks.OrderBy(x => x.Key))
            {
                Printer.Info("Alias: {0}\tDesc: {1}", task.Value.Alias, task.Value.Description);
            }
        }
    }
}