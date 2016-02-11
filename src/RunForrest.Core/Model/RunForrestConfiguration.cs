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

            OnBeforeEachTask = task => { };
            OnAfterEachTask = (task, returnValue) => { };
        }

        public ConsoleColor ConsoleColor { get; set; }

        public string ExecuteAlias { get; set; }

        public bool IsInGroupMode { get; set; }

        public bool IsTimedMode { internal get; set; }

        public bool IsVerbodeMode { internal get; set; }

        public Action<Task> OnBeforeEachTask { internal get; set; }

        public Action<Task, object> OnAfterEachTask { internal get; set; }

        public IEnumerable<Assembly> AdditionalAssembliesToScanForTasks { internal get; set; }

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