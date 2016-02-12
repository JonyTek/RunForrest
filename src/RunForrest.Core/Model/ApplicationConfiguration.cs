using System;
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
                MethodArguments = new string[0],
                ConstructorArguments = new string[0]
            };
            AdditionalAssembliesToScanForTasks = new Assembly[0];
            OnBeforeEachTask = task => { };
            OnAfterEachTask = (task, returnValue) => { };
        }

        public ConsoleColor ConsoleColor { internal get; set; }

        public bool IsInGroupMode { internal get; set; }

        public bool IsTimedMode { internal get; set; }

        public bool IsVerbodeMode { internal get; set; }

        public DefaultArguments DefaultArguments { get; }

        public Action<BasicTask> OnBeforeEachTask { internal get; set; }

        public Action<BasicTask, object> OnAfterEachTask { internal get; set; }

        internal Assembly[] AdditionalAssembliesToScanForTasks { get; set; }

        public DependencyManager Ioc => DependencyManager.Instance;

        internal static ApplicationConfiguration ConfigureApp<T>()
            where T : class
        {
            var appConfiguration = new ApplicationConfiguration();
            var configurations = typeof (T).Assembly.GetConfigurations().ToArray();

            if (!configurations.Any()) return appConfiguration;

            Validate.Configuartions(configurations);

            DependencyManager.Instance.Build();

            var configurer = Instance.Create(configurations.First()) as IConfigureRunForrest;
            configurer.Setup(appConfiguration);

            return appConfiguration;
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

        public ApplicationConfiguration SetAdditionalAssembliesToScanForTasks(Assembly[] assemblies)
        {
            AdditionalAssembliesToScanForTasks = assemblies;
            return this;
        }

        public void ApplyConfigurations()
        {
            Ioc.RegisterConcreteTypeNotAlreadyRegisteredSource();
        }
    }
}