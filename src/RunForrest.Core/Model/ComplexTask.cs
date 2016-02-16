using System;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class ComplexTask<TInstance> : AbstractTask
    {
        private readonly object[] methodArguments;

        private Func<TInstance> Instance { get; set; }

        internal ComplexTask(MethodInfo method, string alias, string description, object[] methodArguments, Func<TInstance> instance)
            : base(method, alias, description)
        {
            Instance = instance;
            Type = typeof(TInstance);
            this.methodArguments = methodArguments;
        }

        internal override void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions)
        {
            configuration.OnBeforeEachTask(this);

            var instance = Instance != null
                ? this.Instance()
                : Util.Instance.Create(Type, instructions.ConstructorArguments);
            var returnValue = Method.Invoke(instance, methodArguments);

            configuration.OnAfterEachTask(this, returnValue);
        }
    }
}