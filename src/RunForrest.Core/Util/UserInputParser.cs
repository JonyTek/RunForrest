using System.Collections.Generic;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal class UserInputParser
    {
        internal static UserInput Parse(string[] arguments)
        {
            var index = 0;
            var instructions = new UserInput();

            if (arguments.Length == 0)
            {
                instructions.Instructions.Add(SwitchType.DisplayHelp, new List<string>());

                return instructions;
            }

            if (ToSwitchType(arguments[index]) == SwitchType.None)
            {
                instructions.Alias = arguments[index];
                index++;
            }

            List<string> currentSwitchArgs = null;
            for (; index < arguments.Length; index++)
            {
                var currentSwitch = ToSwitchType(arguments[index]);
                if (currentSwitch != SwitchType.None)
                {
                    currentSwitchArgs = new List<string>();
                    instructions.Instructions.Add(currentSwitch, currentSwitchArgs);
                }
                else
                {
                    Validate.CurrentArgument(currentSwitchArgs, arguments[index]);

                    currentSwitchArgs.Add(arguments[index]);
                }
            }

            return instructions;
        }

        private static SwitchType ToSwitchType(string argument)
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