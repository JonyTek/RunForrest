using System;
using System.Linq;
using System.Reflection;
using System.Text;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class ComplexTask<TInstance> : AbstractTask
    {
        private readonly object[] methodArguments;

        private readonly Action<AbstractTask, object> onAfterEachTask;

        private readonly Action<AbstractTask> onBeforeEachTask;

        internal ComplexTask(
            MethodInfo method,
            string alias,
            string description,
            object[] methodArguments,
            Func<TInstance> instance,
            int priority,
            Action<AbstractTask> onBeforeEachTask,
            Action<AbstractTask, object> onAfterEachTask
            )
            : base(method, alias, description, priority)
        {
            Instance = instance;
            ExecuteOn = typeof (TInstance);

            this.methodArguments = methodArguments ?? new object[0];
            this.onBeforeEachTask = onBeforeEachTask ?? (task => { });
            this.onAfterEachTask = onAfterEachTask ?? ((task, ret) => { });
        }

        private Func<TInstance> Instance { get; }

        internal override void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions,
            object on = null)
        {
            configuration.OnBeforeEachTask?.Invoke(this);
            onBeforeEachTask?.Invoke(this);

            var instance = on ?? InstanceToExecuteOn(instructions.ConstructorArguments);
            var returnValue = Method.Invoke(instance, ValidateAndConcatMethodParameters(instructions));

            onAfterEachTask?.Invoke(this, returnValue);
            configuration.OnAfterEachTask?.Invoke(this, returnValue);
        }

        private object[] ValidateAndConcatMethodParameters(ApplicationInstructions instructions)
        {
            //We overwrite if passsed from cmd line
            var passedParams = instructions.MethodArguments ?? methodArguments.NullIfEmpty();
            var methodParams = Method.GetParameters();

            if (passedParams.EmptyIfNull().Length == methodParams.EmptyIfNull().Length) return passedParams;

            var error = new StringBuilder()
                .AppendFormat("Invalid parameter list provided, ")
                .AppendFormat("expected {0} parameter(s) but received {1}.", methodArguments.Length,
                    passedParams.Length)
                .AppendFormat("{0}", Environment.NewLine)
                .AppendFormat("Received {0} ", string.Join(", ", passedParams.Select(x => $"\"{x}\"")))
                .AppendFormat("but list looks like {0}.", string.Join(", ", methodParams.Select(x => $"\"{x.Name}\"")));

            throw new InvalidOperationException(error.ToString());
        }

        internal override object InstanceToExecuteOn(object[] constructorArgs)
        {
            return Instance != null ? Instance() : Util.Instance.Create(ExecuteOn, constructorArgs);
        }
    }
}