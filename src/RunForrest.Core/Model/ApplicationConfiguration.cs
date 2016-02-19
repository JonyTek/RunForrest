using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class ApplicationConfiguration
    {
        public ConsoleColor ConsoleColor { internal get; set; }

        internal bool IsInGroupMode { get; set; }

        internal bool IsTimedMode { get; set; }

        internal bool IsVerbodeMode { get; set; }

        public DefaultArguments DefaultArguments { get; }

        public Action<AbstractTask> OnBeforeEachTask { internal get; set; }

        public Action<AbstractTask, object> OnAfterEachTask { internal get; set; }

        private Dictionary<string, Assembly> AdditionalAssembliesToScanForTasks { get; }

        internal Assembly[] AllAssembliesToScan
            => AdditionalAssembliesToScanForTasks.Values.Concat(new[] {configurationLocation}).ToArray();

        private Assembly configurationLocation;

        public Ioc Ioc => Ioc.Container;

        internal ApplicationConfiguration()
        {
            ConsoleColor = ConsoleColor.DarkGreen;
            DefaultArguments = new DefaultArguments
            {
                ExecuteAlias = string.Empty,
                MethodArguments = new string[0],
                ConstructorArguments = new string[0]
            };
            OnBeforeEachTask = task => { };
            OnAfterEachTask = (task, returnValue) => { };
            AdditionalAssembliesToScanForTasks = new Dictionary<string, Assembly>();
        }

        public void AddAdditionalAssemblyToScanForTasks<T>()
            where T : class
        {
            var assembly = typeof (T).Assembly;
            AddAdditionalAssemblyToScanForTasks(assembly);
        }

        private void AddAdditionalAssemblyToScanForTasks(Assembly assembly)
        {
            var assemblyName = assembly.FullName.ToLower();

            if (!AdditionalAssembliesToScanForTasks.ContainsKey(assemblyName))
            {
                AdditionalAssembliesToScanForTasks.Add(assemblyName, assembly);
            }
        }

        public ApplicationConfiguration AddAdditionalAssembliesToScanForTasks(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                AddAdditionalAssemblyToScanForTasks(assembly);
            }
            return this;
        }

        internal static ApplicationConfiguration Bootstrap<TConfiguration>(bool ignoreConfig = false)
            where TConfiguration : class
        {
            var assembly = typeof (TConfiguration).Assembly;
            var configurations = assembly.GetRunForrestConfigurations().ToArray();
            var appConfiguration = new ApplicationConfiguration {configurationLocation = assembly};

            if (configurations.Any() && !ignoreConfig)
            {
                Validate.Configuartions(configurations);

                var configurer = Instance.Create(configurations.First()) as IConfigureRunForrest;
                configurer?.Setup(appConfiguration);
            }

            appConfiguration.ConfigureComplexTasks();

            Ioc.Container.Build();

            return appConfiguration;
        }

        private void ConfigureComplexTasks()
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var configurations = AllAssembliesToScan.SelectMany(x => x.GetComplexTaskConfigurations());

            foreach (var configuration in configurations)
            {
                var configurer = Instance.Create(configuration);
                var setupMethod = configuration.GetMethod("Setup", bindingFlags);
                var taskConfiguration = Instance.Create(setupMethod.GetParameters().First().ParameterType);

                setupMethod.Invoke(configurer, new[] {taskConfiguration});
                var task = ((AbstractComplexTaskConfiguration) taskConfiguration).ToTask();

                TaskCollection.InsertTask(task.Alias, task);
            }
        }


        public ApplicationConfiguration SetIsInGroupMode(bool isInGroupMode)
        {
            IsInGroupMode = isInGroupMode;
            return this;
        }

        public ApplicationConfiguration SetIsInTimedMode(bool isTimedMode)
        {
            IsTimedMode = isTimedMode;
            return this;
        }

        public ApplicationConfiguration SetIsInVerbodeMode(bool isVerbodeMode)
        {
            IsVerbodeMode = isVerbodeMode;
            return this;
        }

        public ApplicationConfiguration SetConsoleColor(ConsoleColor consoleColor)
        {
            ConsoleColor = consoleColor;
            return this;
        }

        public ApplicationConfiguration SetDefaultAlias(string alias)
        {
            DefaultArguments.ExecuteAlias = alias;
            return this;
        }
    }
}