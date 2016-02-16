using System;
using System.Reflection;
using RunForrest.Core.Ioc;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
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

        public ComplexTaskConfiguration<TInstance> WithDescription(string desc)
        {
            this.description = desc;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnInstance(Func<TInstance> instance)
        {
            this.instance = instance;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnMethodWithName(string name)
        {
            method = typeof(TInstance).GetMethodByName(name);

            if (method == null)
            {
                throw new InvalidOperationException(
                    string.Format("No methond found '{0}' on type {1}", name,
                        typeof(TInstance).Name));
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
}