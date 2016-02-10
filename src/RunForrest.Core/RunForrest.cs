using System;
using System.Linq;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core
{
    public static class RunForrest
    {
        public static void Run<T>(string[] arguments)
            where T : class
        {
            try
            { 
                var config = TryConfigureApplication<T>();

                TaskCollection.Initialise<T>(config);

                InstructionBuilder.Build(arguments).Execute();
            }
            catch (Exception ex)
            {
                Printer.Error(ex.Message);
            }
        }

        private static RunForrestConfiguration TryConfigureApplication<T>()
        {
            var configuration = RunForrestConfiguration.Instance;
            var configurations = typeof(T).Assembly.GetConfigurations().ToArray();

            if (!configurations.Any()) return configuration;

            Validate.Configuartions(configurations);

            var configurer = Instance.Create(configurations.First(), null) as IConfigureRunForrest;

            configurer.Configure(configuration);

            return configuration;
        }
    }
}