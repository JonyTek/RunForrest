using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal static class StringExtensions
    {
        internal static InstructionType ToInstructionType(this string argument)
        {
            switch (argument.ToLower())
            {
                case "-c":
                case "-constructor":
                    return InstructionType.Constructor;
                case "-m":
                case "-method":
                    return InstructionType.Method;
                case "-v":
                case "-verbose":
                    return InstructionType.Verbose;
                case "-t":
                case "-timed":
                    return InstructionType.Timed;
                case "-l":
                case "-list":
                    return InstructionType.DisplayList;
                case "-h":
                case "-help":
                    return InstructionType.DisplayHelp;
                case "-g":
                case "-group":
                    return InstructionType.Group;
                case "-p":
                case "-parra":
                    return InstructionType.Parallel;
                default:
                    return InstructionType.None;
            }
        }

        internal static bool IsASwitch(this string argument)
        {
            return argument.ToInstructionType() != InstructionType.None;
        }
    }
}