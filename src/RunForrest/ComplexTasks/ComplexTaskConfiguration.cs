using RunForrest.Core.Model;
using RunForrest.Core.Util;
using RunForrest.Services;

namespace RunForrest.ComplexTasks
{
    public class ComplexTaskConfiguration : IConfigureComplexTask<IComplexTask>
    {
        public void Setup(ComplexTaskConfiguration<IComplexTask> configuration)
        {
            configuration.Ioc.Register<IService, Service>();
            configuration.Ioc.Register<IComplexTask, MyComplexTask>();

            configuration.WithAlias("alias");
            configuration.WithDescription("my sescription");
            configuration.OnMethodWithName("sayhello");
            configuration.WithMethodArguments(new object[0]);
        }
    }
}