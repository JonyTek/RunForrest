using System;
using Autofac;
using Autofac.Features.ResolveAnything;

namespace RunForrest.Core.Util
{
    public class Ioc
    {
        #region Singleton

        private static Ioc instance;

        private static readonly object LockObject = new object();

        public static Ioc Container
        {
            get
            {
                lock (LockObject)
                {
                    if (instance == null)
                        instance = new Ioc();

                    return instance;
                }
            }
        }

        #endregion

        private readonly ContainerBuilder builder;

        private IContainer container;

        private Ioc()
        {
            builder = new ContainerBuilder();
        }

        internal void Build()
        {
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            container = builder.Build();
        }

        public void RegisterSingleton<TInterface, TConcrete>()
            where TConcrete : TInterface
        {
            builder.RegisterType<TConcrete>().As<TInterface>().SingleInstance();
        }

        public void Register<TInterface, TConcrete>()
           where TConcrete : TInterface
        {
            builder.RegisterType<TConcrete>().As<TInterface>();
        }

        public object Resolve(Type type)
        {
            object resolved;
            using (var scope = container.BeginLifetimeScope())
            {
                resolved = scope.Resolve(type);
            }

            return resolved;
        }

        public TInterface Resolve<TInterface>()
        {
            TInterface resolved;
            using (var scope = container.BeginLifetimeScope())
            {
                resolved = scope.Resolve<TInterface>();
            }

            return resolved;
        }
    }
}