using System;
using System.Diagnostics;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteSingleTaskInstructions : ExecuteTimedTaskBase, IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, ApplicationConfiguration configuration)
        {
            Validate.ExecuteAlias(instructions.ExecuteAlias);
            var isTimedMode = instructions.TimedMode || configuration.IsTimedMode;

            try
            {
                PrintStartTime(isTimedMode);
                var task = TaskCollection.SelectTask(instructions.ExecuteAlias.Alias);
                task.Execute(configuration, instructions);
            }
            finally
            {
                PrintEndTime(isTimedMode);
            }
        }
    }
}