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
                var setup = TryConfigureApplication<T>();

                TaskCollection.Initialise<T>(setup);

                UserInputParser.Parse(arguments).Execute();
            }
            catch (Exception ex)
            {
                Printer.Error(ex.Message);
            }
        }

        private static RunForrestConfiguration TryConfigureApplication<T>()
        {
            var configurations = typeof(T).Assembly.GetConfigurations().ToArray();

            if (!configurations.Any()) return RunForrestConfiguration.Instance;

            Validate.Configuartions(configurations);

            var configurer = Instance.Create(configurations.First(), null) as IConfigureRunForrest;

            configurer.Setup(RunForrestConfiguration.Instance);

            return RunForrestConfiguration.Instance; 
        }
    }
}