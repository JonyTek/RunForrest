using System;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest
{
    public class Configuration : IConfigureRunForrest<Configuration>
    {
        public void Configure(RunForrestConfiguration configuration)
        {
            configuration.OnBeforeEachTask = task =>
            {
                Printer.Print(ConsoleColor.DarkGreen, "Starting {0}", task.Alias);
            };

            configuration.OnAfterEachTask = (task, returnValue) =>
            {
                Printer.Print(ConsoleColor.DarkGreen, "Completed {0}", task.Alias);
                if (returnValue != null)
                {
                    Printer.Print(ConsoleColor.Blue, "Completed with return value:  {0}", returnValue);
                }
            };

            configuration.IsTimedMode = true;
            configuration.IsVerbodeMode = true;
        }
    }
}