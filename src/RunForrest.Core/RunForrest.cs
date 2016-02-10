using System;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core
{
    public static class RunForrest
    {
        public static void Run<T>(string[] arguments)
            where T : class
        {
            try
            {
                TaskCollection.Initialise<T>();

                InstructionBuilder.Build(arguments).Execute();
            }
            catch (Exception ex)
            {
                Printer.Error(ex.Message);
            }
        }

        public static void OnBeforeEachTask(Action<Task> onBeforeEachTask)
        {
            TaskHooks.OnBeforeEachTask = onBeforeEachTask;
        }

        public static void OnAfterEachTask(Action<Task, object> onAfterEachTask)
        {
            TaskHooks.OnAfterEachTask = onAfterEachTask;
        }
    }
}