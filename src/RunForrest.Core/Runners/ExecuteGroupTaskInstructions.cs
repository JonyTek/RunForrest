using System.Threading.Tasks;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteGroupTaskInstructions : ExecuteTimedTaskBase, IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, ApplicationConfiguration configuration)
        {
            Validate.ExecuteAlias(instructions.ExecuteAlias);

            var isTimedMode = instructions.TimedMode || configuration.IsTimedMode;
            var group = TaskCollection.SelectTaskGroup(instructions.ExecuteAlias.Alias);

            Validate.TaskGroup(group);

            try
            {
                PrintStartTime(isTimedMode);

                if (instructions.ParallelMode)
                {
                    ExecuteASync(group, instructions, configuration);
                }
                else
                {
                    ExecuteSync(group, instructions, configuration);
                }
            }
            finally
            {
                PrintEndTime(isTimedMode);
            }
        }

        private static void ExecuteSync(TaskGroup group, ApplicationInstructions instructions, ApplicationConfiguration configuration)
        {
            var groupInstance = group.ExecuteOnInstance(instructions);

            foreach (var task in group.Tasks)
            {
                task.Execute(configuration, instructions, groupInstance);
            }
        }

        private static void ExecuteASync(TaskGroup group, ApplicationInstructions instructions, ApplicationConfiguration configuration)
        {
            var groupInstance = group.ExecuteOnInstance(instructions);

            Parallel.ForEach(group.Tasks, task =>
            {
                task.Execute(configuration, instructions, groupInstance);
            });
        }
    }
}