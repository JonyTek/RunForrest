using System;
using RunForrest.Core.Model;

namespace RunForrest.Core.Runners
{
    internal class RunListInstructions : IRunInstructions
    {
        public void Run(ApplicationInstructions instructions)
        {
            if (!string.IsNullOrEmpty(instructions.TaskAlias))
            {
                throw new ArgumentException("Invalid arguments. Cannot list a task.");
            }

            if (instructions.Instructions.Keys.Count > 1)
            {
                throw new ArgumentException("Invalid arguments. -l cannot be used with any other switches.");
            }

            TaskCollection.PrintList();
        }
    }
}