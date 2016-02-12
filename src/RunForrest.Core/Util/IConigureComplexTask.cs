using System;
using System.Linq.Expressions;
using System.Reflection;
using RunForrest.Core.Attributes;
using RunForrest.Core.Ioc;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public class ComplexTaskConfiguration<TInstance>
    {
        private string alias;

        private Func<TInstance> instance;

        private MethodInfo method;

        private object[] constructorArguments;

        private object[] methodArguments;

        public DependencyManager Ioc => DependencyManager.Instance;

        public ComplexTaskConfiguration<TInstance> WithAlias(string alias)
        {
            this.alias = alias;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnInstance(Func<TInstance> instance)
        {
            this.instance = instance;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnMethodWithAlias(string onMethod)
        {
            //Need to get method by alias -- task
            method = typeof (TInstance).GetMethod(onMethod);
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithConstructorArguments(object[] arguments)
        {
            constructorArguments = arguments;
            return this;
        }

        public ComplexTaskConfiguration<TInstance> WithMethodArguments(object[] arguments)
        {
            methodArguments = arguments;
            return this;
        }

        public ITask<TInstance> ToTask()
        {
            return new ComplexTask<TInstance>();
        }
    }

    public class ComplexTask<TInstance> : ITask<TInstance>
    {
        public TInstance InstanceToCall { get; }

        public MethodInfo Method { get; set; }

        public void Execute(ApplicationConfiguration configuration)
        {

        }
    }

    public interface ITask<out TInstance>
    {
        TInstance InstanceToCall { get; }

        MethodInfo Method { get; set; }

        void Execute(ApplicationConfiguration configuration);
    }

    public interface IConigureComplexTask<T>
    {
        void Setup(ComplexTaskConfiguration<T> configuration);
    }
}