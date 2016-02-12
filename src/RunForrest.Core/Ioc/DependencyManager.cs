using System;
using Autofac;
using Autofac.Features.ResolveAnything;

namespace RunForrest.Core.Ioc
{
    public class DependencyManager
    {
        #region Singleton

        private static DependencyManager instance;

        private static readonly object LockObject = new object();

        public static DependencyManager Instance
        {
            get
            {
                lock (LockObject)
                {
                    if (instance == null)
                        instance = new DependencyManager();

                    return instance;
                }
            }
        }

        #endregion

        internal ContainerBuilder Builder { get; }

        internal IContainer Container { get; set; }

        private DependencyManager()
        {
            Builder = new ContainerBuilder();
        }

        internal void Build()
        {
            Container = Builder.Build();
        }

        internal void RegisterConcreteTypeNotAlreadyRegisteredSource()
        {
            Builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
        }

        public void RegisterSingleton<TInterface, TConcrete>()
            where TConcrete : TInterface
        {
            Builder.RegisterType<TConcrete>().As<TInterface>().SingleInstance();
        }

        public void Register<TInterface, TConcrete>()
           where TConcrete : TInterface
        {
            Builder.RegisterType<TConcrete>().As<TInterface>();
        }

        public TInterface Resolve<TInterface>()
        {
            TInterface resolved;
            using (var scope = Container.BeginLifetimeScope())
            {
                resolved = scope.Resolve<TInterface>();
            }

            return resolved;
        }

        public T Resolve<T>(Type type)
            where T : class
        {
            T resolved;
            using (var scope = Container.BeginLifetimeScope())
            {
                resolved = scope.Resolve(type) as T;
            }

            return resolved;
        }

        public object Resolve(Type type)
        {
            object resolved;
            using (var scope = Container.BeginLifetimeScope())
            {
                resolved = scope.Resolve(type);
            }

            return resolved;
        }
    }
}