using System;
using RunForrest.Core.Model;

namespace RunForrest.Core.Runners
{
    internal class ExecuteNullInstructions : IExecuteInstructions
    {
        public void Execute(ApplicationInstructions instructions, ApplicationConfiguration configuration)
        {
            Console.WriteLine("Unkown Application Type!");
        }
    }
}