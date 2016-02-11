using System;
using System.Linq;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteListInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, RunForrestConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(instructions.ExecuteAlias))
            {
                throw new ArgumentException("Invalid arguments. Cannot list a task.");
            }

            if (instructions.Instructions.Keys.Count > 1)
            {
                throw new ArgumentException("Invalid arguments. -l cannot be used with any other switches.");
            }

            PrintTaskList();
            PrintTaskGroupList();
        }

        internal static void PrintTaskList()
        {
            Printer.Info("TASKS:");
            foreach (var task in TaskCollection.GetTasks().OrderBy(x => x.Key))
            {
                Printer.Info("taskalias - {0}\t\t{1}", task.Value.Alias, task.Value.Description);
            }

            Printer.Info(string.Empty);
            Printer.Info("<alias> -h for more details\n");
        }

        internal static void PrintTaskGroupList()
        {
            Printer.Info("TASK GROUPS:");
            foreach (var group in TaskCollection.GetTaskGroups().OrderBy(x => x.Key))
            {
                Printer.Info("groupalias - {0}\t\t{1}", group.Key, group.Value.Description);
            }

            Printer.Info(string.Empty);
            Printer.Info("<groupalias> -g -h for more details");
        }
    }
}