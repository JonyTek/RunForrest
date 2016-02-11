using System;

namespace RunForrest.Core.Util
{
    public static class BoolExtension
    {
        internal static bool ExecuteIfTrue(this bool flag, Action action, bool returnValue = false)
        {
            if (flag)
            {
                action();
            }

            return returnValue;
        }

        internal static bool ExecuteIfFalse(this bool flag, Action action, bool returnValue = true)
        {
            return ExecuteIfTrue(!flag, action, returnValue);
        }
    }
}