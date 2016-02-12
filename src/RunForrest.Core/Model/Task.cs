using System;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Ioc;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class Task
    {
        private readonly Type type;

        private readonly MethodInfo method;

        private readonly ParameterInfo[] parameters;

        internal int Priority { get; }

        internal Task(Type type, MethodInfo method)
        {
            this.type = type;
            this.method = method;            
            this.parameters = method.GetParameters();

            Alias = method.GetTaskAlias();
            Priority = method.GetTaskPriority();
            Description = method.GetTaskDescription();
        }

        public string Alias { get; private set; }

        public string Description { get; private set; }

        public bool ReturnsValue => method.ReturnType.Name == "Void";

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

                return string.Format("<appname> {0} {1}", method.GetTaskAlias(), usage);
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
                    method.ReturnType.Name, method.Name, string.Join(", ", parmters));
            }
        }

        internal void Execute(ApplicationConfiguration configuration, object[] constructorArgs = null, object[] methodArgs = null)
        {
            configuration.OnBeforeEachTask(this);

            Console.ForegroundColor = configuration.ConsoleColor;

            var instance = DependencyManager.Instance.Resolve(type);

            var returnValue = method.Invoke(Instance.Create(type, constructorArgs), methodArgs);

            configuration.OnAfterEachTask(this, returnValue);

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}