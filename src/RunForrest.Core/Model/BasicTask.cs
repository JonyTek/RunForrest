using System;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class BasicTask : ITask<object>
    {
        private readonly Type type;

        public MethodInfo Method { get; set; }

        public object InstanceToCall
        {
            get { return Instance.Create(type, con); }
        }

        private readonly ParameterInfo[] parameters;

        internal int Priority { get; }

        internal BasicTask(Type type, MethodInfo method)
        {
            this.type = type;
            this.Method = method;            
            this.parameters = method.GetParameters();

            Alias = method.GetTaskAlias();
            Priority = method.GetTaskPriority();
            Description = method.GetTaskDescription();
        }

        public string Alias { get; private set; }

        public string Description { get; private set; }

        public bool ReturnsValue => Method.ReturnType.Name == "Void";

        public string UsageExample
        {
            get
            {
                var usage = string.Empty;

                if (parameters.Any())
                {
                    usage = "-m <";
                    usage = parameters.ToArray()
                        .Aggregate(usage, (current, parameter) => current + (parameter.Name + "> "));
                }

                return string.Format("<appname> {0} {1}", Method.GetTaskAlias(), usage);
            }
        }

        internal string MethodSignature
        {
            get
            {
                var parmters = from x in parameters.ToList()
                    select string.Format("{0} {1}",
                        x.ParameterType.Name, x.Name);

                return string.Format("public {0} {1}({2}){{ }}",
                    Method.ReturnType.Name, Method.Name, string.Join(", ", parmters));
            }
        }

        internal void Execute(ApplicationConfiguration configuration, object[] constructorArgs = null, object[] methodArgs = null)
        {
            configuration.OnBeforeEachTask(this);

            var returnValue = Method.Invoke(Instance.Create(type, constructorArgs), methodArgs);

            configuration.OnAfterEachTask(this, returnValue);
        }

        public void Execute(ApplicationConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}