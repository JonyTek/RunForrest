using System;
using System.Reflection;

namespace RunForrest.Core.Model
{
    public class ComplexTask<TInstance> : AbstractTask
    {
        private readonly object[] methodArguments;

        private Func<TInstance> Instance { get; set; }

        private readonly Action<AbstractTask> onBeforeEachTask;

        private readonly Action<AbstractTask, object> onAfterEachTask;

        internal ComplexTask(MethodInfo method, string alias, string description, object[] methodArguments,
            Func<TInstance> instance, int priority, Action<AbstractTask> onBeforeEachTask,
            Action<AbstractTask, object> onAfterEachTask)
            : base(method, alias, description, priority)
        {
            Instance = instance;
            ExecuteOn = typeof (TInstance);

            this.methodArguments = methodArguments;
            this.onBeforeEachTask = onBeforeEachTask ?? (task => { });
            this.onAfterEachTask = onAfterEachTask ?? ((task, ret) => { });
        }

        internal override void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions,
            object on = null)
        {
            configuration.OnBeforeEachTask(this);
            onBeforeEachTask(this);

            var instance = on ?? InstanceToExecuteOn(instructions.ConstructorArguments);
            var returnValue = Method.Invoke(instance, methodArguments ?? instructions.MethodArguments);

            onAfterEachTask(this, returnValue);
            configuration.OnAfterEachTask(this, returnValue);
        }

        internal override object InstanceToExecuteOn(object[] constructorArgs)
        {
            return Instance != null ? Instance() : Util.Instance.Create(ExecuteOn, constructorArgs);
        }
    }
}