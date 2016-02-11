using System;
using RunForrest.Core.Model;

namespace RunForrest.Core.Runners
{
    internal class ExecuteNullInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, RunForrestConfiguration configuration)
        {
            Console.WriteLine("Unkown Application Type!");
        }
    }
}