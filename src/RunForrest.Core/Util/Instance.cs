﻿using System;

namespace RunForrest.Core.Util
{
    internal static class Instance
    {
        internal static object Create(Type type, params object[] args)
        {
            try
            {
                return Activator.CreateInstance(type, args);
            }
            catch (MissingMethodException ex)
            {
                var message =
                    string.Format(
                        "Failed to create type '{0}', please ensure you provide correct instantiation parameters",
                        type.FullName);

                throw new MissingMethodException(message, ex);
            }
        }
    }
}