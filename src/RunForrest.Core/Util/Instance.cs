using System;

namespace RunForrest.Core.Util
{
    internal static class Instance
    {
        internal static object Create(Type type, params object[] args)
        {
            try
            {
                return args == null ? Ioc.Container.Resolve(type) : Activator.CreateInstance(type, args);
            }
            catch (MissingMethodException ex)
            {
                var message =
                    string.Format(
                        "Failed to create type '{0}', please ensure you provide correct constructor arguments. -c arg0 arg1",
                        type.FullName);

                throw new MissingMethodException(message, ex);
            }
        }

        internal static object Create(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException ex)
            {
                var message = string.Format("Failed to create type '{0}'.", type.FullName);

                throw new MissingMethodException(message, ex);
            }
        }
    }
}