using System;
using System.Reflection;
using RunForrest.Core.Util;
using Ioc = RunForrest.Core.Util.Ioc;

namespace RunForrest.Core.Model
{
    public class ComplexTaskConfiguration<TInstance> : AbstractComplexTaskConfiguration
    {
        private string alias;

        private Func<TInstance> instance;

        private MethodInfo method;

        private object[] methodArguments;

        private string description;

        private int priority;

        private Action<AbstractTask> onBeforeEachTask;

        private Action<AbstractTask, object> onAfterEachTask;

        public Ioc Ioc => Ioc.Container;

        public ComplexTaskConfiguration<TInstance> WithAlias(string alias)
        {
            this.alias = alias;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithDescription(string description)
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
            var type = typeof (TInstance);
            method = type.GetMethodByName(name);
            Validate.MethodName(method, name, type);
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithPriority(int priority)
        {
            this.priority = priority;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithOnBeforeTask(Action<AbstractTask> onBeforeEachTask)
        {
            this.onBeforeEachTask = onBeforeEachTask;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithOnAfterTask(Action<AbstractTask, object> onAfterEachTask)
        {
            this.onAfterEachTask = onAfterEachTask;
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
                throw new InvalidOperationException(
                    string.Format("No method found for {0}", method));
            if (string.IsNullOrEmpty(alias))
                throw new InvalidOperationException(
                    string.Format("No alias specified for {0}", method));

            return new ComplexTask<TInstance>(method, alias, description, methodArguments, instance, priority,
                onBeforeEachTask, onAfterEachTask);
        }
    }
}