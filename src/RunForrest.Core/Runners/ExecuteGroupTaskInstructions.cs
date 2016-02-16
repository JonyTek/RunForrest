using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteGroupTaskInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, ApplicationConfiguration configuration)
        {
            if (string.IsNullOrEmpty(instructions.ExecuteAlias.Alias))
            {
                throw new ArgumentException("Invalid arguments. Please specify as task alias.");
            }

            var taskCollection = TaskCollection.SelectTaskGroup(instructions.ExecuteAlias.Alias);

            var sw = Stopwatch.StartNew();
            var isTimedMode = instructions.TimedMode || configuration.IsTimedMode;

            if (isTimedMode)
            {
                Printer.Info("Starting execution at: {0}", DateTime.Now.ToString("O"));
            }

            try
            {
                if (instructions.ParallelMode)
                {
                    Parallel.ForEach(taskCollection.Tasks, task =>
                    {
                        task.Execute(configuration, instructions);
                    });
                }
                else
                {
                    foreach (var task in taskCollection.Tasks)
                    {
                        task.Execute(configuration, instructions);
                    }
                }
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