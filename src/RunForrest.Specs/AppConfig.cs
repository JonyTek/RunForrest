using System;
using RunForrest.Core.Ioc;
using RunForrest.Core.Model;
using RunForrest.Core.Util;
using RunForrest.Specs.Services;
using RunForrest.Specs.Tasks;

namespace RunForrest.Specs
{
    public class AppConfig : IConfigureRunForrest
    {
        public void Setup(ApplicationConfiguration configuration)
        {
            //configuration.IsInGroupMode = true;
            configuration.IsTimedMode = true;
            configuration.IsVerbodeMode = true;
            configuration.ConsoleColor = ConsoleColor.DarkRed;

            RegisterDependencies(configuration.Ioc);
        }

        private static void RegisterDependencies(DependencyManager manager)
        {
            manager.Register<IService, Service>();
            manager.Register<IRepository, Repository>();
            manager.Register<IComplexTask, ComplexTask>();
        }
    }
}