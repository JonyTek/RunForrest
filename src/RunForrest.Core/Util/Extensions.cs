using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal static class Extensions
    {
        internal static string GetTaskAlias(this MethodInfo method)
        {
            var alias = method.GetCustomAttribute<TaskAttribute>().Alias ?? method.Name;

            return alias.ToLower();
        }

        internal static string GetTaskDescription(this MethodInfo method)
        {
            return method.GetCustomAttribute<TaskAttribute>().Description;
        }

        internal static IEnumerable<Task> ScanForTasks(this Assembly assembly)
        {
            return from type in assembly.GetTypes()
                from method in type.GetMethods()
                where method.IsTask()
                select new Task(type, method);
        }

        internal static bool IsTask(this MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof (TaskAttribute));
        }

        internal static IEnumerable<Type> GetConfigurations(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => typeof (IConfigureRunForrest).IsAssignableFrom(x));
        }
    }
}