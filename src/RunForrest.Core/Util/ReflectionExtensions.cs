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
            if (method.GetCustomAttribute<TaskAttribute>() == null) return 0;

            return method.GetCustomAttribute<TaskAttribute>().Priority;
        }

        internal static string GetTaskAlias(this MethodInfo method)
        {
            if (method.GetCustomAttribute<TaskAttribute>() == null) return null;

            var alias = method.GetCustomAttribute<TaskAttribute>().Alias ?? method.Name;

            return alias.ToLower();
        }

        internal static string GetTaskGroupDescription(this MethodInfo method)
        {
            if (method.GetCustomAttribute<TaskAttribute>() == null) return null;

            return method.GetCustomAttribute<TaskAttribute>().Description;
        }

        internal static string GetTaskGroupAlias(this Type group)
        {
            if (group.GetCustomAttribute<TaskGroupAttribute>() == null) return null;

            var alias = group.GetCustomAttribute<TaskGroupAttribute>().Alias ?? group.Name;

            return alias.ToLower();
        }

        internal static string GetTaskGroupDescription(this Type type)
        {
            if (type.GetCustomAttribute<TaskGroupAttribute>() == null) return null;

            return type.GetCustomAttribute<TaskGroupAttribute>().Description;
        }

        internal static IEnumerable<AbstractTask> ScanForSingleTasks(this Assembly assembly)
        {
            return from type in assembly.GetTypes()
                   from method in type.GetMethods()
                   where method.IsTask()
                   select new BasicTask(type, method);
        }

        internal static IEnumerable<TaskGroup> ScanForTaskGroups(this Assembly assembly)
        {
            var groups = assembly.GetTypes().Where(x => x.IsTaskGroup());

            return from taskGroup in groups
                let tasks = taskGroup.GetMethods().Where(x => x.IsTask()).Select(x => new BasicTask(taskGroup, x))
                select new TaskGroup
                {
                    Alias = taskGroup.GetTaskGroupAlias(),
                    Description = taskGroup.GetTaskGroupDescription(),
                    Tasks = tasks
                };
        }

        internal static MethodInfo GetMethodByName(this Type type, string name)
        {
            return
                type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(x => string.Equals(x.Name, name,
                        StringComparison.CurrentCultureIgnoreCase));
        }

        internal static bool IsTask(this MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(TaskAttribute));
        }

        internal static bool IsTaskGroup(this Type type)
        {
            return Attribute.IsDefined(type, typeof(TaskGroupAttribute));
        }

        internal static IEnumerable<Type> GetRunForrestConfigurations(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => typeof(IConfigureRunForrest).IsAssignableFrom(x));
        }

        internal static IEnumerable<Type> GetComplexTaskConfigurations(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => typeof(IConfigureComplexTask).IsAssignableFrom(x));
        }
    }
}