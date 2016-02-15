using System;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Ioc;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public abstract class AbstractComplexTaskConfiguration
    {
        internal abstract AbstractTask ToTask();
    }

    public class ComplexTaskConfiguration<TInstance> : AbstractComplexTaskConfiguration
    {
        private string alias;

        private Func<TInstance> instance;

        private MethodInfo method;

        private object[] methodArguments;

        private string description;

        public DependencyManager Ioc => DependencyManager.Instance;

        public ComplexTaskConfiguration<TInstance> WithAlias(string alias)
        {
            this.alias = alias;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithdescriptionTaskConfiguration(string description)
        {
            this.description = description;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnInstance(Func<TInstance> instance)
        {
            this.instance = instance;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnMethodWithName(string name)
        {
            method = typeof (TInstance).GetMethodByName(name);

            if (method == null)
            {
                throw new InvalidOperationException(
                    string.Format("No methond found '{0}' on type {1}", name,
                        typeof (TInstance).Name));
            }

            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithMethodArguments(object[] arguments)
        {
            methodArguments = arguments;
            return this;
        }

        internal override AbstractTask ToTask()
        {
            if (method == null)
                throw new InvalidOperationException(string.Format("No method found for {0}", method));
            if (string.IsNullOrEmpty(alias))
                throw new InvalidOperationException(string.Format("No alias specified for {0}", method));

            return new ComplexTask<TInstance>(method, alias, description, methodArguments, instance);
        }
    }

    public class ComplexTask<TInstance> : AbstractTask
    {
        private readonly object[] methodArguments;
        
        private Func<TInstance> Instance { get; set; }

        internal ComplexTask(MethodInfo method, string alias, string description, object[] methodArguments, Func<TInstance> instance)
            :base(method, alias, description)
        {
            Instance = instance;
            Type = typeof (TInstance);
            this.methodArguments = methodArguments;
        }

        internal override void Execute(ApplicationConfiguration configuration, ApplicationInstructions instructions)
        {
            configuration.OnBeforeEachTask(this);

            var instance = Instance != null
                ? Instance()
                : Util.Instance.Create(Type, instructions.ConstructorArguments);
            var returnValue = Method.Invoke(instance, methodArguments);

            configuration.OnAfterEachTask(this, returnValue);
        }
    }

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
            Description = description;
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

    public interface IConigureComplexTask<TInstance> : IConigureComplexTask
    {
        void Setup(ComplexTaskConfiguration<TInstance> configuration);
    }

    public interface IConigureComplexTask
    {
    }
}