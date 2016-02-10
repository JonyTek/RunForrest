using System;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal static class TaskHooks
    {
        internal static Action<Task> OnBeforeEachTask { get; set; }

        internal static Action<Task, object> OnAfterEachTask { get; set; }
    }
}