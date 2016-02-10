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
            var alias = method.GetCustomAttribute<RunnableAttribute>().Alias ?? method.Name;

            return alias.ToLower();
        }

        internal static string GetTaskDescription(this MethodInfo method)
        {
            return method.GetCustomAttribute<RunnableAttribute>().Description;
        }

        internal static IEnumerable<Task> ScanForTasks(this Assembly assembly)
        {
            return from type in assembly.GetTypes()
                from method in type.GetMethods()
                where method.IsRunnable()
                select Task.Create(type, method);
        }

        internal static bool IsRunnable(this MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof (RunnableAttribute));
        }

        internal static string Signature(this MethodInfo method)
        {
            var parameters = from x in method.GetParameters()
                select string.Format("{0} {1}",
                    x.ParameterType.Name, x.Name);

            return string.Format("{0} {1}({2})",
                method.ReturnType.Name, method.Name, string.Join(", ", parameters));
        }
    }
}