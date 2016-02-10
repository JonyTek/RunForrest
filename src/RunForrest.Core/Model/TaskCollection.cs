using System;
using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal static class TaskCollection
    {
        private static readonly Dictionary<string, Task> Tasks = new Dictionary<string, Task>();

        private static void Add(string alias, Task task)
        {
            var lowerAlias = alias.ToLower();

            if (Tasks.ContainsKey(lowerAlias))
            {
                throw new InvalidOperationException(string.Format("Task '{0}' with same alias already exists", alias));
            }

            Tasks.Add(lowerAlias, task);
        }

        internal static void Initialise<T>()
        {
            var assembly = typeof (T).Assembly;
            foreach (var task in assembly.ScanForTasks())
            {
                Add(task.Alias, task);
            }
        }

        internal static Task Get(string alias)
        {
            var lowerAlias = alias.ToLower();

            if (!Tasks.ContainsKey(lowerAlias))
            {
                throw new KeyNotFoundException(string.Format("No task found by the alias of '{0}'", alias));
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