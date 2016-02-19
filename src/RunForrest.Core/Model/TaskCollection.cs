using System;
using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal static class TaskCollection
    {
        private static readonly Dictionary<string, AbstractTask> Tasks = new Dictionary<string, AbstractTask>();

        private static readonly Dictionary<string, TaskGroup> TaskGroups = new Dictionary<string, TaskGroup>();

        internal static void InsertTask(string alias, AbstractTask task)
        {
            var lowerAlias = alias.ToLower();

            if (Tasks.ContainsKey(lowerAlias))
            {
                throw new InvalidOperationException($"Task '{alias}' with same alias already exists");
            }

            Tasks.Add(lowerAlias, task);
        }

        private static void InsertTaskGroup(string alias, TaskGroup group)
        {
            var lowerAlias = alias.ToLower();

            if (TaskGroups.ContainsKey(lowerAlias))
            {
                throw new InvalidOperationException($"Task group '{alias}' with same alias already exists");
            }

            TaskGroups.Add(lowerAlias, group);
        }

        internal static void Initialise(ApplicationConfiguration config)
        {
            var assemblies = config.AllAssembliesToScan;

            foreach (var task in assemblies.SelectMany(x => x.ScanForSingleTasks()))
            {
                InsertTask(task.Alias, task);
            }

            foreach (var taskGroup in assemblies.SelectMany(x => x.ScanForTaskGroups()))
            {
                InsertTaskGroup(taskGroup.Alias, taskGroup);
            }
        }

        internal static AbstractTask SelectTask(string alias)
        {
            var lowerAlias = alias.ToLower();

            if (!Tasks.ContainsKey(lowerAlias))
            {
                throw new KeyNotFoundException($"No task found by the alias of '{alias}'");
            }

            return Tasks[lowerAlias];
        }

        internal static TaskGroup SelectTaskGroup(string alias)
        {
            var lowerAlias = alias.ToLower();

            if (!TaskGroups.ContainsKey(lowerAlias))
            {
                throw new KeyNotFoundException($"No task group found by the alias of '{alias}'");
            }

            return TaskGroups[lowerAlias];
        }

        internal static Dictionary<string, AbstractTask> GetTasks()
        {
            return Tasks;
        }

        internal static Dictionary<string, TaskGroup> GetTaskGroups()
        {
            return TaskGroups;
        }
    }
}