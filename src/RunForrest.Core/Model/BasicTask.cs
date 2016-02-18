using System;
using System.Linq;
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

        internal override void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions)
        {
            configuration.OnBeforeEachTask(this);

            var instance = Instance.Create(ExecuteOn, instructions.ConstructorArguments);
            var returnValue = Method.Invoke(instance, instructions.MethodArguments);

            configuration.OnAfterEachTask(this, returnValue);
        }

    }
}