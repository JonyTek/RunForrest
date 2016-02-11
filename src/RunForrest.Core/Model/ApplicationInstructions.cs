using System;
using System.Collections.Generic;
using RunForrest.Core.Runners;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal class ApplicationInstructions
    {
        private readonly RunForrestConfiguration configuration;

        internal ApplicationInstructions(RunForrestConfiguration configuration)
        {
            this.configuration = configuration;
            Instructions = new Dictionary<SwitchType, List<string>>();
        }

        internal string ExecuteAlias { get; set; }

        internal Dictionary<SwitchType, List<string>> Instructions;        

        internal bool ListMode => Instructions.ContainsKey(SwitchType.DisplayList);

        internal bool HelpMode => Instructions.ContainsKey(SwitchType.DisplayHelp);

        internal bool GroupMode => Instructions.ContainsKey(SwitchType.Group);

        internal bool TimedMode => Instructions.ContainsKey(SwitchType.Timed);

        internal bool VerbodeMode => Instructions.ContainsKey(SwitchType.Verbose);
        
        internal bool ParallelMode => Instructions.ContainsKey(SwitchType.Parallel);

        internal ApplicationMode ApplicationMode { get; set; }

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

                if (GroupMode) return new ExecuteGroupTaskInstructions();

                return new ExecuteSingleTaskInstructions();
            }
        }

        internal void Run()
        {
            try
            {
                Runner.Execute(this, configuration);
            }
            catch (Exception ex)
            {
               Printer.Error(ex, VerbodeMode);
            }
        }
    }
}