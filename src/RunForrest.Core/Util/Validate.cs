using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal static class Validate
    {
        internal static void CurrentArgument(List<string> currentSwitchArgs, string argument)
        {
            if (currentSwitchArgs == null)
            {
                throw new ArgumentException(
                    $"You have provided invalid arguments. Argument at postion [1] '{argument}' is invalid.");
            }
        }

        internal static void Configuartions(Type[] configurations)
        {
            if (configurations.Length > 1)
            {
                throw new InvalidOperationException(
                    $"Cannot specify multiple configuration points in a single application. You have specified {configurations.Length}");
            }
        }

        internal static void ExecuteAlias(ExecutionAlias executeAlias)
        {
            if (string.IsNullOrEmpty(executeAlias?.Alias))
            {
                throw new ArgumentException(
                    "Invalid arguments. Please specify as task alias.");
            }
        }

        internal static void TaskGroup(TaskGroup group)
        {
            if (!group.Tasks.Any())
            {
                throw new ApplicationException(
                    $"Now tasks associated with group {group.Alias}");
            }
        }

        internal static void MethodName(MethodInfo method, string name, Type type)
        {
            if (method == null)
            {
                throw new InvalidOperationException(
                    $"No methond found '{name}' on type {type.Name}");
            }
        }
    }
}