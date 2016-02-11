using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteGroupTaskInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, RunForrestConfiguration configuration)
        {
            if (string.IsNullOrEmpty(instructions.ExecuteAlias))
            {
                throw new ArgumentException("Invalid arguments. Please specify as task alias.");
            }

            var taskCollection = TaskCollection.SelectTaskGroup(instructions.ExecuteAlias);

            var sw = Stopwatch.StartNew();
            var isTimedMode = instructions.TimedMode || configuration.IsTimedMode;


            if (instructions.ParallelMode)
            {
                Parallel.ForEach(taskCollection.Tasks, task =>
                {
                    task.Execute(configuration, instructions.ConstructorArguments, instructions.MethodArguments);
                });                
            }
            else
            {
                foreach (var task in taskCollection.Tasks)
                {
                    task.Execute(configuration, instructions.ConstructorArguments, instructions.MethodArguments);
                }
            }
            

            /* 
            
            TODO: Add group help -g -h - print details
            for -h we need to print if constructor arg?
            
            add OnBeforeGroupExecution  and after hook
            
            */

            
            //if (isTimedMode)
            //{
            //    Printer.Info("Starting execution at: {0}", DateTime.Now.ToString("O"));
            //}

            //try
            //{
            //    TaskCollection.SelectTask(instructions.TaskAlias)
            //        .Execute(instructions.ConstructorArguments, instructions.MethodArguments);
            //}
            //finally
            //{
            //    sw.Stop();

            //    if (isTimedMode)
            //    {
            //        Printer.Info("Finished execution at: {0}", DateTime.Now.ToString("O"));
            //        Printer.Info("Total execution time: {0}ms", sw.ElapsedMilliseconds);
            //    }
            //}
        }
    }
}