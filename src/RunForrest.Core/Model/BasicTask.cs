using System;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class BasicTask : AbstractTask
    {
        internal BasicTask(Type executeOn, MethodInfo method)
            : base(method)
        {
            ExecuteOn = executeOn;
        }

        internal override void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions,
            object on = null)
        {
            configuration.OnBeforeEachTask(this);

            var instance = on ?? InstanceToExecuteOn(instructions.ConstructorArguments);
            var returnValue = Method.Invoke(instance, instructions.MethodArguments);

            configuration.OnAfterEachTask(this, returnValue);
        }

        internal override object InstanceToExecuteOn(object[] constructorArgs)
        {
            return Instance.Create(ExecuteOn, constructorArgs);
        }
    }
}