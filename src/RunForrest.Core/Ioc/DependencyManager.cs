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

        private ContainerBuilder Builder { get; }

        private IContainer Container { get; set; }

        private DependencyManager()
        {
            Builder = new ContainerBuilder();
        }

        internal void Build()
        {
            Builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            Container = Builder.Build();
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

        public object Resolve(Type type)
        {
            object resolved;
            using (var scope = Container.BeginLifetimeScope())
            {
                resolved = scope.Resolve(type);
            }

            return resolved;
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
    }
}