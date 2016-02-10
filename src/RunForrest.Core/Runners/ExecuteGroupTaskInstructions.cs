using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteGroupTaskInstructions : IExecuteInstructions
    {
        public void Execute(UserInput instructions)
        {
            if (string.IsNullOrEmpty(instructions.Alias))
            {
                throw new ArgumentException("Invalid arguments. Please specify as task alias.");
            }

            var taskCollection = TaskCollection.SelectTaskGroup(instructions.Alias);

            var sw = Stopwatch.StartNew();
            var isTimedMode = instructions.TimedMode || RunForrestConfiguration.Instance.IsTimedMode;


            if (instructions.ParallelMode)
            {
                Parallel.ForEach(taskCollection.Tasks, x =>
                {
                    x.Execute();
                });                
            }
            else
            {
                foreach (var task in taskCollection.Tasks)
                {
                    task.Execute();
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