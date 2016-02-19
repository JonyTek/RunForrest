using System;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public abstract class AbstractTask
    {
        protected Type ExecuteOn { get; set; }

        internal MethodInfo Method { get; }

        protected ParameterInfo[] MethodParameters => Method.GetParameters();

        public int Priority { get; protected set; }

        public string Alias { get; protected set; }

        public string Description { get; protected set; }

        public bool ReturnsValue => Method.ReturnType.Name == "Void";

        internal AbstractTask(MethodInfo method)
            : this(method, method.GetTaskAlias(), method.GetTaskGroupDescription(), method.GetTaskPriority())
        {
        }

        internal AbstractTask(MethodInfo method, string alias, string description, int priority = 0)
        {
            Method = method;
            Alias = alias;
            Priority = priority;
            Description = description ?? Constants.NoDescriptionText;
        }

        public string UsageExample
        {
            get
            {
                var usage = string.Empty;

                if (MethodParameters.Any())
                {
                    usage = "-m <";
                    usage = MethodParameters.ToArray()
                        .Aggregate(usage, (current, parameter) => current + (parameter.Name + "> "));
                }

                return $"<appname> {Method.GetTaskAlias()} {usage}";
            }
        }

        internal string MethodSignature
        {
            get
            {
                var parmters = from x in MethodParameters.ToList()
                    select $"{x.ParameterType.Name} {x.Name}";

                return $"public {Method.ReturnType.Name} {Method.Name}({string.Join(", ", parmters)}){{ }}";
            }
        }

        internal abstract void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions,
            object on = null);

        internal abstract object InstanceToExecuteOn(object[] constructorArgs);
    }
}