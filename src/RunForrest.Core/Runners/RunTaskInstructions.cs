using System;
using System.Diagnostics;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class RunTaskInstructions : IRunInstructions
    {
        public void Run(ApplicationInstructions instructions)
        {
            if (string.IsNullOrEmpty(instructions.TaskAlias))
            {
                throw new ArgumentException("Invalid arguments. Please specify as task alias.");
            }

            var sw = Stopwatch.StartNew();

            if (instructions.TimedMode)
            {
                Printer.Info("Starting execution at: {0}", DateTime.Now.ToString("O"));
            }

            try
            {
                var task = TaskCollection.Select(instructions.TaskAlias);

                task.Execute(instructions.ConstructorArguments, instructions.MethodArguments);
            }
            finally
            {
                sw.Stop();

                if (instructions.TimedMode)
                {
                    Printer.Info("Finished execution at: {0}", DateTime.Now.ToString("O"));
                    Printer.Info("Total execution time: {0}ms", sw.ElapsedMilliseconds);
                }
            }
        }
    }
}