using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class RunForrestConfiguration
    {
        internal RunForrestConfiguration()
        {
            ConsoleColor = ConsoleColor.DarkGreen;

            MethodArguments = new string[0];
            ConstructorArguments = new string[0];
            AdditionalAssembliesToScanForTasks = new Assembly[0];

            OnBeforeEachTask = task => { };
            OnAfterEachTask = (task, returnValue) => { };
        }

        public ConsoleColor ConsoleColor { internal get; set; }

        public string ExecuteAlias { internal get; set; }

        public bool IsInGroupMode { internal get; set; }

        public bool IsTimedMode { internal get; set; }

        public bool IsVerbodeMode { internal get; set; }

        public string[] ConstructorArguments { internal get; set; }

        public string[] MethodArguments { internal get; set; }2

        public Action<Task> OnBeforeEachTask { internal get; set; }

        public Action<Task, object> OnAfterEachTask { internal get; set; }

        public Assembly[] AdditionalAssembliesToScanForTasks { internal get; set; }

        internal static RunForrestConfiguration ConfigureApp<T>()
            where T : class 
        {
            var appConfiguration = new RunForrestConfiguration();
            var configurations = typeof(T).Assembly.GetConfigurations().ToArray();

            if (!configurations.Any()) return appConfiguration;

            Validate.Configuartions(configurations);

            var configurer = Instance.Create(configurations.First(), null) as IConfigureRunForrest;

            configurer?.Setup(appConfiguration);

            return appConfiguration;
        }
    }
}