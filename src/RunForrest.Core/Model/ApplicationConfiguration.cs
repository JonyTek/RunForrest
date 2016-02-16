﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RunForrest.Core.Ioc;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    public class ApplicationConfiguration
    {
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

        public ConsoleColor ConsoleColor { internal get; set; }

        public bool IsInGroupMode { internal get; set; }

        public bool IsTimedMode { internal get; set; }

        public bool IsVerbodeMode { internal get; set; }

        public DefaultArguments DefaultArguments { get; }

        public Action<AbstractTask> OnBeforeEachTask { internal get; set; }

        public Action<AbstractTask, object> OnAfterEachTask { internal get; set; }

        internal Dictionary<string, Assembly> AdditionalAssembliesToScanForTasks { get; set; }

        internal Assembly[] AllAssembliesToScan
            => AdditionalAssembliesToScanForTasks.Values.Concat(new[] {configurationLocation}).ToArray();

        private Assembly configurationLocation;

        public DependencyManager Ioc => DependencyManager.Instance;

        public void AddAdditionalAssemblyToScanForTasks<T>()
            where T : class
        {
            var assembly = typeof(T).Assembly;
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

        public ApplicationConfiguration SetAdditionalAssembliesToScanForTasks(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                AddAdditionalAssemblyToScanForTasks(assembly);
            }
            return this;
        }

        internal static ApplicationConfiguration ConfigureApp<TConfiguration>()
            where TConfiguration : class
        {
            var assembly = typeof (TConfiguration).Assembly;
            var configurations = assembly.GetRunForrestConfigurations().ToArray();
            var appConfiguration = new ApplicationConfiguration {configurationLocation = assembly};

            if (!configurations.Any()) return appConfiguration;

            Validate.Configuartions(configurations);

            DependencyManager.Instance.Build();

            var configurer = Instance.Create(configurations.First()) as IConfigureRunForrest;
            configurer.Setup(appConfiguration);

            appConfiguration.ConfigureComplexTasks();

            return appConfiguration;
        }

        private void ConfigureComplexTasks()
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var configurations = AllAssembliesToScan.SelectMany(x => x.GetComplexTaskConfigurations());

            foreach (var configuration in configurations)
            {
                var confgurer = Instance.Create(configuration, null);
                var setupMethod = configuration.GetMethod("Setup", bindingFlags);
                var taskConfiguration = Instance.Create(setupMethod.GetParameters().First().ParameterType, null);

                setupMethod.Invoke(confgurer, new[] { taskConfiguration });
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

        public void ApplyConfigurations()
        {
            Ioc.RegisterConcreteTypeNotAlreadyRegisteredSource();
        }
    }
}