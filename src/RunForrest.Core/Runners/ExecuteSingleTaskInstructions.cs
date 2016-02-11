using System;
using System.Diagnostics;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteSingleTaskInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, RunForrestConfiguration configuration)
        {
            if (string.IsNullOrEmpty(instructions.ExecuteAlias.Alias))
            {
                throw new ArgumentException("Invalid arguments. Please specify as task alias.");
            }

            var sw = Stopwatch.StartNew();
            var isTimedMode = instructions.TimedMode || configuration.IsTimedMode;

            if (isTimedMode)
            {
                Printer.Info("Starting execution at: {0}", DateTime.Now.ToString("O"));
            }

            try
            {
                var task = TaskCollection.SelectTask(instructions.ExecuteAlias.Alias);
                task.Execute(configuration, instructions.ConstructorArguments, instructions.MethodArguments);
            }
            finally
            {
                sw.Stop();

                if (isTimedMode)
                {
                    Printer.Info("Finished execution at: {0}", DateTime.Now.ToString("O"));
                    Printer.Info("Total execution time: {0}ms", sw.ElapsedMilliseconds);
                }
            }
        }
    }
}