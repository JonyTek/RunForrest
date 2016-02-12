using System;
using System.Linq.Expressions;
using System.Reflection;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public class ComplexTaskConfiguration<TInstance>
        where TInstance : class
    {
        private string alias;

        private TInstance instance;

        private MethodInfo method;

        private object[] constructorArguments;

        private object[] methodArguments;

        public ComplexTaskConfiguration<TInstance> WithAlias(string alias)
        {
            this.alias = alias;

            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnInstance(TInstance instance)
        {
            this.instance = instance;

            return this;
        }

        public ComplexTaskConfiguration<TInstance> OnMethod(string onMethod)
        {
            method = typeof(TInstance).GetMethod(onMethod);

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

        public ITask ToTask()
        {
            return new ComplexTask();
        }
    }

    public class ComplexTask : ITask
    {
        public void Execute(RunForrestConfiguration configuration)
        {
        }
    }

    public interface ITask
    {
        void Execute(RunForrestConfiguration configuration);
    }

    public interface IConigureComplexTask<T>
        where T : class
    {
        void Setup(ComplexTaskConfiguration<T> configuration);
    }

    public class TaskConfiguration : IConigureComplexTask<TaskContainer>
    {
        public void Setup(ComplexTaskConfiguration<TaskContainer> configuration)
        {
            configuration.WithAlias("alias")
                .OnMethod("MyTask")
                .OnInstance(new TaskContainer())
                .WithConstructorArguments(new object[0])
                .WithMethodArguments(new object[0]);
        }
    }

    public class TaskContainer
    {
        public void MyTask()
        {
            Console.WriteLine("YOOO!!");
        }
    }
}