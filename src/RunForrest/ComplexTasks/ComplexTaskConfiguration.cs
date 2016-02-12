using System;
using RunForrest.Core.Ioc;
using RunForrest.Core.Util;
using RunForrest.Services;

namespace RunForrest.ComplexTasks
{
    public class ComplexTaskConfiguration : IConigureComplexTask<IComplexTask>
    {
        public void Setup(ComplexTaskConfiguration<IComplexTask> configuration)
        {
            RegisterDependencies(configuration.Ioc);

            configuration.WithAlias("alias");
            configuration.OnInstanceOf<IComplexTask>();
            configuration.OnMethodWithAlias("complextask");

            //configuration.OnInstance(() => new TaskContainer());
            //configuration.WithConstructorArguments(new object[0]);
            //configuration.WithMethodArguments(new object[0]);
        }

        public void RegisterDependencies(DependencyManager manager)
        {
            manager.Register<IService, Service>();
            manager.Register<IComplexTask, ComplexTask>();
        }
    }
}