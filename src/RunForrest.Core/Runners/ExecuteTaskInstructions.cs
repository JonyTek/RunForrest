using System;
using System.Diagnostics;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteTaskInstructions : IExecuteInstructions
    {
        public void Execute(UserInput instructions)
        {
            if (string.IsNullOrEmpty(instructions.TaskAlias))
            {
                throw new ArgumentException("Invalid arguments. Please specify as task alias.");
            }

            var sw = Stopwatch.StartNew();
            var isTimedMode = instructions.TimedMode || RunForrestConfiguration.Instance.IsTimedMode;

            if (isTimedMode)
            {
                Printer.Info("Starting execution at: {0}", DateTime.Now.ToString("O"));
            }

            try
            {
                TaskCollection.Select(instructions.TaskAlias)
                    .Execute(instructions.ConstructorArguments, instructions.MethodArguments);
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