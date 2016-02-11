using System;
using System.Linq;
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
                var config = RunForrestConfiguration.ConfigureApp<T>();
                
                Printer.Configure(config);

                TaskCollection.Initialise<T>(config);

                InstructionParser.ParseInstructions(arguments, config).Run();
            }
            catch (Exception ex)
            {
                Printer.Error(ex.Message);
            }
        }
    }
}