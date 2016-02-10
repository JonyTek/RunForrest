using System;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class RunHelpInstructions : IRunInstructions
    {
        public void Run(ApplicationInstructions instructions)
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

            var task = TaskCollection.Get(instructions.TaskAlias);

            Printer.Info("Alias: {0}\t", task.Alias);
            Printer.Info("Desc:  {0}\t", task.Description);
            Printer.Info("Sign:  {0}\t", task.Signature);
        }
    }
}