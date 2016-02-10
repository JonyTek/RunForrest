using System;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class Task
    {
        private Type type;

        private MethodInfo method;

        private Task()
        {
        }

        public string Signature => method.Signature();

        public string Alias { get; private set; }

        public string Description { get; private set; }

        internal static Task Create(Type type, MethodInfo method)
        {
            return new Task
            {
                type = type,
                method = method,
                Alias = method.GetTaskAlias(),
                Description = method.GetTaskDescription()
            };
        }

        internal void Execute(object[] constructorArgs = null, object[] methodArgs = null)
        {
            if (TaskHooks.OnBeforeEachTask != null) TaskHooks.OnBeforeEachTask(this);

            Console.ForegroundColor = ConsoleColor.Green;

            var returnValue = method.Invoke(Instance.Create(type, constructorArgs), methodArgs);

            if (TaskHooks.OnAfterEachTask != null) TaskHooks.OnAfterEachTask(this, returnValue);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}