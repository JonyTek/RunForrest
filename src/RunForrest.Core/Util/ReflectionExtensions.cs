using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Attributes;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal static class ReflectionExtensions
    {
        internal static int GetTaskPriority(this MethodInfo method)
        {
            return method.GetCustomAttribute<TaskAttribute>().Priority;
        }

        internal static string GetTaskAlias(this MethodInfo method)
        {
            var alias = method.GetCustomAttribute<TaskAttribute>().Alias ?? method.Name;

            return alias.ToLower();
        }

        internal static string GetTaskDescription(this MethodInfo method)
        {
            return method.GetCustomAttribute<TaskAttribute>().Description;
        }

        internal static string GetTaskGroupAlias(this Type group)
        {
            var alias = group.GetCustomAttribute<TaskGroupAttribute>().Alias ?? group.Name;

            return alias.ToLower();
        }

        internal static string GetTaskDescription(this Type Type)
        {
            return Type.GetCustomAttribute<TaskGroupAttribute>().Description;
        }

        internal static IEnumerable<Task> ScanForSingleTasks(this Assembly assembly)
        {
            return from type in assembly.GetTypes()
                   from method in type.GetMethods()
                   where method.IsTask()
                   select new Task(type, method);
        }

        internal static IEnumerable<TaskGroup> ScanForTaskGroups(this Assembly assembly)
        {
            var groups = assembly.GetTypes().Where(x => x.IsTaskGroup());

            return from taskGroup in groups
                let tasks = taskGroup.GetMethods().Where(x => x.IsTask()).Select(x => new Task(taskGroup, x))
                select new TaskGroup
                {
                    Alias = taskGroup.GetTaskGroupAlias(),
                    Description = taskGroup.GetTaskDescription(),
                    Tasks = tasks
                };
        }

        internal static bool IsTask(this MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(TaskAttribute));
        }

        internal static bool IsTaskGroup(this Type type)
        {
            return Attribute.IsDefined(type, typeof(TaskGroupAttribute));
        }

        internal static IEnumerable<Type> GetConfigurations(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => typeof (IConfigureRunForrest).IsAssignableFrom(x));
        }
    }
}