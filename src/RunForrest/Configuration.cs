using System;
using RunForrest.Core.Ioc;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest
{
    public class Configuration : IConfigureRunForrest
    {
        public void Setup(ApplicationConfiguration configuration)
        {
            configuration.OnBeforeEachTask = task =>
            {
                Printer.Print(ConsoleColor.DarkGreen, "Starting {0}", task.Alias);
            };

            configuration.OnAfterEachTask = (task, returnValue) =>
            {
                if (returnValue != null)
                {
                    Printer.Print(ConsoleColor.Blue, "{0}", returnValue);
                }
                Printer.Print(ConsoleColor.DarkGreen, "Completed {0}", task.Alias);
            };

            RegisterIoc(configuration.Ioc);

            configuration
                //.SetIsInGroupMode(true)
                .SetIsInTimedMode(true)
                .SetIsInVerbodeMode(true)
                .SetDefaultAlias("group")
                .SetConsoleColor(ConsoleColor.DarkGreen)
                .ApplyConfigurations();

            //configuration.IsTimedMode = true;
            //configuration.IsVerbodeMode = true;

            //configuration.AdditionalAssembliesToScanForTasks = new List<Assembly>();

        }

        private void RegisterIoc(DependencyManager dependencyManager)
        {
            dependencyManager.Register<IAmAnInterface, AmAClass>();
        }
    }

    public interface IAmAnInterface
    {
        string Name { get; }
    }

    public class AmAClass : IAmAnInterface
    {
        public string Name => "Hi.";
    }
}