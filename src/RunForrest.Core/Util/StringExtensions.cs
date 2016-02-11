using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal static class StringExtensions
    {
        internal static SwitchType ToSwitchType(this string argument)
        {
            switch (argument.ToLower())
            {
                case "-c":
                case "-constructor":
                    return SwitchType.Constructor;
                case "-m":
                case "-method":
                    return SwitchType.Method;
                case "-v":
                case "-verbose":
                    return SwitchType.Verbose;
                case "-t":
                case "-timed":
                    return SwitchType.Timed;
                case "-l":
                case "-list":
                    return SwitchType.DisplayList;
                case "-h":
                case "-help":
                    return SwitchType.DisplayHelp;
                case "-g":
                case "-group":
                    return SwitchType.Group;
                case "-p":
                case "-parra":
                    return SwitchType.Parallel;
                default:
                    return SwitchType.None;
            }
        }
    }
}