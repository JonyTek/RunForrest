using System;
using System.Collections.Generic;

namespace RunForrest.Core.Util
{
    internal static class Validate
    {
        internal static void CurrentArgument(List<string> currentSwitchArgs, string argument)
        {
            if (currentSwitchArgs == null)
            {
                throw new ArgumentException(
                    string.Format("You have provided invalid arguments. Argument at postion [1] '{0}' is invalid.",
                        argument));
            }
        }

        internal static void Configuartions(Type[] configurations)
        {
            if (configurations.Length > 1)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Cannot specify multiple configuration points in a single application. You have specified {0}",
                        configurations.Length));
            }
        }
    }
}