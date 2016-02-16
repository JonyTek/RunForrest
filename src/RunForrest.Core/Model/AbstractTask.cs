using System;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public abstract class AbstractTask
    {
        internal AbstractTask(MethodInfo method)
            : this(method, method.GetTaskAlias(), method.GetTaskDescription(), method.GetTaskPriority())
        {
        }

        internal AbstractTask(MethodInfo method, string alias, string description, int priority = 0)
        {
            Method = method;
            Alias = alias;
            Priority = priority;
            Description = description ?? Constants.NoDescriptionText;
        }

        protected Type Type { get; set; }

        internal MethodInfo Method { get; }

        protected ParameterInfo[] MethodParameters => Method.GetParameters();

        public int Priority { get; protected set; }

        public string Alias { get; protected set; }

        public string Description { get; protected set; }

        public bool ReturnsValue => Method.ReturnType.Name == "Void";

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

                return string.Format("<appname> {0} {1}", Method.GetTaskAlias(), usage);
            }
        }

        internal string MethodSignature
        {
            get
            {
                var parmters = from x in MethodParameters.ToList()
                               select string.Format("{0} {1}",
                                   x.ParameterType.Name, x.Name);

                return string.Format("public {0} {1}({2}){{ }}",
                    Method.ReturnType.Name, Method.Name, string.Join(", ", parmters));
            }
        }

        internal abstract void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions);
    }
}