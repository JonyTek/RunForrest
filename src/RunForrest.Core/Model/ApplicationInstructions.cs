using System;
using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Runners;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal class ApplicationInstructions
    {
        private readonly ApplicationConfiguration configuration;

        internal ApplicationInstructions(ApplicationConfiguration configuration)
        {
            this.configuration = configuration;
            ExecuteAlias = new ExecutionAlias();
            Instructions = new Dictionary<InstructionType, Instruction>();
        }

        internal ExecutionAlias ExecuteAlias { get; set; }

        internal Dictionary<InstructionType, Instruction> Instructions;        

        internal bool TimedMode => Instructions.ContainsKey(InstructionType.Timed);

        internal bool VerbodeMode => Instructions.ContainsKey(InstructionType.Verbose);
        
        internal bool ParallelMode => Instructions.ContainsKey(InstructionType.Parallel);

        internal ApplicationMode ApplicationMode
        {
            get
            {
                if (Instructions.ContainsKey(InstructionType.DisplayHelp))
                {
                    return ApplicationMode.Help;
                }
                if (Instructions.ContainsKey(InstructionType.DisplayList))
                {
                    return ApplicationMode.List;
                }
                if(string.IsNullOrEmpty(ExecuteAlias.Alias))
                {
                    return ApplicationMode.Help;
                }

                return Instructions.ContainsKey(InstructionType.Group) ? ApplicationMode.Group : ApplicationMode.Single;
            }
        }

        internal void SetInstructions(InstructionType instructionType, Instruction instruction)
        {
            if (Instructions.ContainsKey(instructionType))
                Instructions[instructionType] = instruction;
            else
                Instructions.Add(instructionType, instruction);
        }

        internal object[] ConstructorArguments
            => !Instructions.ContainsKey(InstructionType.Constructor)
                ? null
                : Instructions[InstructionType.Constructor].Arguments.ToArray();

        internal object[] MethodArguments
            => !Instructions.ContainsKey(InstructionType.Method)
                ? null
                : Instructions[InstructionType.Method].Arguments.ToArray();

        private IExecuteInstructions Runner
        {
            get
            {
                switch (ApplicationMode)
                {
                    case ApplicationMode.List:
                        return new ExecuteListInstructions();
                    case ApplicationMode.Help:
                        return new ExecuteHelpInstructions();
                    case ApplicationMode.Group:
                        return new ExecuteGroupTaskInstructions();
                    case ApplicationMode.Single:
                        return new ExecuteSingleTaskInstructions();
                    default:
                        return new ExecuteNullInstructions();
                }
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