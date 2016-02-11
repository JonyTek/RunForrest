using System;
using System.Linq;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteHelpInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, RunForrestConfiguration configuration)
        {
            if (instructions.Instructions.Values.Count(x => x.InstructionsFrom == InstructionsFrom.Console) > 1)
            {
                throw new ArgumentException("Invalid arguments. -h cannot be used with any other switches.");
            }

            if (string.IsNullOrEmpty(instructions.ExecuteAlias.Alias) || instructions.ExecuteAlias.InstructionsFrom == InstructionsFrom.Configuration)
            {
                Printer.PrintHelp();

                return;
            }

            var task = TaskCollection.SelectTask(instructions.ExecuteAlias.Alias);

            Printer.Info("{0} - {1}", task.UsageExample, task.Description);
        }
    }
}