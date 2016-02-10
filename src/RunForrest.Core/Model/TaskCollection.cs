﻿using System;
using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal static class TaskCollection
    {
        private static readonly Dictionary<string, Task> Tasks = new Dictionary<string, Task>();

        private static readonly Dictionary<string, TaskGroup> TaskGroups = new Dictionary<string, TaskGroup>();

        private static void InsertTask(string alias, Task task)
        {
            var lowerAlias = alias.ToLower();

            if (Tasks.ContainsKey(lowerAlias))
            {
                throw new InvalidOperationException(
                    string.Format("Task '{0}' with same alias already exists", alias));
            }

            Tasks.Add(lowerAlias, task);
        }

        private static void InsertTaskGroup(string alias, TaskGroup group)
        {
            var lowerAlias = alias.ToLower();

            if (TaskGroups.ContainsKey(lowerAlias))
            {
                throw new InvalidOperationException(
                    string.Format("Task group '{0}' with same alias already exists", alias));
            }

            TaskGroups.Add(lowerAlias, group);
        }

        internal static void Initialise<T>(RunForrestConfiguration config)
        {
            var assemblies = new[] {typeof (T).Assembly}.ToList();

            if (config.AdditionalAssembliesToScanForTasks != null)
            {
                assemblies.AddRange(config.AdditionalAssembliesToScanForTasks);
            }

            foreach (var task in assemblies.SelectMany(x => x.ScanForSingleTasks()))
            {
                InsertTask(task.Alias, task);
            }

            foreach (var taskGroup in assemblies.SelectMany(x => x.ScanForTaskGroups()))
            {
                InsertTaskGroup(taskGroup.Alias, taskGroup);
            }
        }

        internal static Task SelectTask(string alias)
        {
            var lowerAlias = alias.ToLower();

            if (!Tasks.ContainsKey(lowerAlias))
            {
                throw new KeyNotFoundException(
                    string.Format("No task found by the alias of '{0}'", alias));
            }

            return Tasks[lowerAlias];
        }

        internal static TaskGroup SelectTaskGroup(string alias)
        {
            var lowerAlias = alias.ToLower();

            if (!TaskGroups.ContainsKey(lowerAlias))
            {
                throw new KeyNotFoundException(
                    string.Format("No task group found by the alias of '{0}'", alias));
            }

            return TaskGroups[lowerAlias];
        }

        internal static bool ContainsTaskAlias(string alias)
        {
            return Tasks.ContainsKey(alias.ToLower());
        }

        internal static Dictionary<string, Task> GetTasks()
        {
            return Tasks;
        }

        internal static Dictionary<string, TaskGroup> GetTaskGroups()
        {
            return TaskGroups;
        }
    }
}