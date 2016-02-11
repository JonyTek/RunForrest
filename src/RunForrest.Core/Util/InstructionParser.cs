using System.Collections.Generic;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal class InstructionParser
    {
        internal static ApplicationInstructions ParseInstructions(string[] arguments, RunForrestConfiguration configuration)
        { 
            var index = 0;
            var instructions = new ApplicationInstructions(configuration);

            if (arguments.Length == 0)
            {
                instructions.Instructions.Add(SwitchType.DisplayHelp, new List<string>());

                return instructions;
            }

            if (arguments[index].ToSwitchType() == SwitchType.None)
            {
                instructions.ExecuteAlias = arguments[index];
                index++;
            }

            List<string> currentSwitchArgs = null;
            for (; index < arguments.Length; index++)
            {
                var currentSwitch = arguments[index].ToSwitchType();
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

        
    }
}