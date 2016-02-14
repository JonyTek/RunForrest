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
            configuration.Ioc.Register<IService, Service>();
            configuration.Ioc.Register<IComplexTask, MyComplexTask>();

            configuration.WithAlias("alias");
            configuration.OnMethodWithName("sayhello");
            configuration.WithMethodArguments(new object[0]);

            //configuration.OnInstance(() => new MyComplexTask(new Service()));
        }
    }
}