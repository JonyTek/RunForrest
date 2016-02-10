using System;
using System.Collections.Generic;
using RunForrest.Core.Runners;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal class UserInput
    {
        internal UserInput()
        {
            Instructions = new Dictionary<SwitchType, List<string>>();
        }

        internal string Alias { get; set; }

        internal Dictionary<SwitchType, List<string>> Instructions;        

        internal bool ListMode => Instructions.ContainsKey(SwitchType.DisplayList);

        internal bool HelpMode => Instructions.ContainsKey(SwitchType.DisplayHelp);

        internal bool TimedMode => Instructions.ContainsKey(SwitchType.Timed);

        internal bool VerbodeMode => Instructions.ContainsKey(SwitchType.Verbose);

        internal bool GroupMode => Instructions.ContainsKey(SwitchType.Group);

        internal bool ParallelMode => Instructions.ContainsKey(SwitchType.Parallel);

        internal object[] ConstructorArguments
            => !Instructions.ContainsKey(SwitchType.Constructor)
                ? null
                : Instructions[SwitchType.Constructor]
                    .ToArray();

        internal object[] MethodArguments
            => !Instructions.ContainsKey(SwitchType.Method)
                ? null
                : Instructions[SwitchType.Method]
                    .ToArray();

        internal IExecuteInstructions Runner
        {
            get
            {
                if (ListMode) return new ExecuteListInstructions();

                if (HelpMode) return new ExecuteHelpInstructions();

                if(GroupMode) return new ExecuteGroupTaskInstructions();

                return new ExecuteSingleTaskInstructions();
            }
        }

        internal void Execute()
        {
            try
            {
                Runner.Execute(this);
            }
            catch (Exception ex)
            {
               Printer.Error(ex, VerbodeMode);
            }
        }
    }
}