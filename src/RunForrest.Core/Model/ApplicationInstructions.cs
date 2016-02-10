using System;
using System.Collections.Generic;
using RunForrest.Core.Runners;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal class ApplicationInstructions
    {
        internal ApplicationInstructions()
        {
            Instructions = new Dictionary<SwitchType, List<string>>();
        }

        internal string TaskAlias { get; set; }

        internal Dictionary<SwitchType, List<string>> Instructions;        

        internal bool ListMode => Instructions.ContainsKey(SwitchType.DisplayList);

        internal bool HelpMode => Instructions.ContainsKey(SwitchType.DisplayHelp);

        internal bool TimedMode => Instructions.ContainsKey(SwitchType.Timed);

        internal bool VerbodeMode => Instructions.ContainsKey(SwitchType.Verbose);

        internal string[] ConstructorArguments
            => !Instructions.ContainsKey(SwitchType.Constructor)
                ? null
                : Instructions[SwitchType.Constructor]
                    .ToArray();

        internal string[] MethodArguments
            => !Instructions.ContainsKey(SwitchType.Method)
                ? null
                : Instructions[SwitchType.Method]
                    .ToArray();

        internal IRunInstructions Runner
        {
            get
            {
                if (ListMode) return new RunListInstructions();

                if (HelpMode) return new RunHelpInstructions();

                return new RunTaskInstructions();
            }
        }

        internal void Execute()
        {
            try
            {
                Runner.Run(this);
            }
            catch (Exception ex)
            {
                if (VerbodeMode)
                {
                    Printer.Error(ex);
                }
                else
                {
                    Printer.Error(ex.InnerException.Message);
                }
            }
        }
    }
}