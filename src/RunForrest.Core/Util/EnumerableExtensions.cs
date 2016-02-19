using System;
using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public static class EnumerableExtensions
    {
        internal static object ToInstanceToExecuteOn(this IEnumerable<AbstractTask> collection, ApplicationInstructions instructions)
        {
            return collection.First().InstanceToExecuteOn(instructions.ConstructorArguments);
        }
    }
}