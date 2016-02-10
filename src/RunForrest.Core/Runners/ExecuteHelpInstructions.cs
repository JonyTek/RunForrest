using System;
using System.Linq;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteHelpInstructions : IExecuteInstructions
    {
        public void Execute(UserInput instructions)
        {
            if (instructions.Instructions.Keys.Count > 1)
            {
                throw new ArgumentException("Invalid arguments. -h cannot be used with any other switches.");
            }

            if (string.IsNullOrEmpty(instructions.TaskAlias))
            {
                Printer.PrintHelp();

                return;
            }

            var task = TaskCollection.Select(instructions.TaskAlias);

            Printer.Info(task.UsageExample());
        }
    }
}