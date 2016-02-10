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
    }
}