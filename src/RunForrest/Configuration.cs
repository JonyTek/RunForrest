﻿using System;
using System.Collections.Generic;
using System.Reflection;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest
{
    public class Configuration : IConfigureRunForrest
    {
        public void Setup(RunForrestConfiguration configuration)
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

            configuration.IsTimedMode = true;
            configuration.IsVerbodeMode = true;
            configuration.AdditionalAssembliesToScanForTasks = new List<Assembly>();
        }
    }
}