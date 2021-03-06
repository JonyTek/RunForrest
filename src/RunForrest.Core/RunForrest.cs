﻿using System;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Core
{
    public static class RunForrest
    {
        public static void Run<T>(string[] arguments)
            where T : class
        {
            try
            {
                var config = ApplicationConfiguration.Bootstrap<T>();

                Printer.Configure(config);

                TaskCollection.Initialise(config);

                var instructions = new InstructionBuilder(arguments, config).Build();

                instructions.Run();
            }
            catch (Exception ex)
            {
                Printer.Error(ex.Message);
            }
        }
    }
}