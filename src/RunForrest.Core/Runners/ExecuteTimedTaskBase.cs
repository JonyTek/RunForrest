using System;
using System.Diagnostics;
using RunForrest.Core.Util;

namespace RunForrest.Core.Runners
{
    internal class ExecuteTimedTaskBase
    {
        protected Stopwatch Sw;

        protected void PrintStartTime(bool print)
        {
            if (!print) return;
            Sw = Stopwatch.StartNew();
            Printer.Info("Starting execution at: {0}", DateTime.Now.ToString("O"));
        }

        protected void PrintEndTime(bool print)
        {
            Sw.Stop();
            if (!print) return;
            Printer.Info("Finished execution at: {0}", DateTime.Now.ToString("O"));
            Printer.Info("Total execution time: {0}ms", Sw.ElapsedMilliseconds);
        }
    }
}