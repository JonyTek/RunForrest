using System;
using System.Runtime.InteropServices.WindowsRuntime;
using RunForrest.Core.Util;
using RunnForrest = RunForrest.Core.RunForrest;

namespace RunForrest
{
    class Program
    {
        static void Main(string[] args)
        {
            RunnForrest.OnBeforeEachTask(task =>
            {
                Printer.Info("Starting {0}", task.Alias);
            });

            RunnForrest.OnAfterEachTask((task, returnValue) =>
            {
                Printer.Info("Completed {0}", task.Alias);
                if (returnValue != null)
                {
                    Printer.Info("Completed with return value:  {0}", returnValue);
                }
            });

            RunnForrest.Run<Program>(args);
        }
    }
}
