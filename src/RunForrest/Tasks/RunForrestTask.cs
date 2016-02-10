using System;
using RunForrest.Core.Model;

namespace RunForrest.Tasks
{
    public class RunForrestTask
    {
        [Runnable("run", "Run Forrest, run!!")]
        public void RunTask()
        {
            Console.WriteLine("Run Forrest, run!");
        }

        [Runnable("dowhat", "Run Forrest, run!!")]
        public void DoWhatTask(string verb)
        {
            Console.WriteLine("Run Forrest, {0}!", verb);
        }

        [Runnable("ex", "Throw an exception")]
        public void ExceptionTask()
        {
            throw new Exception("INVALID OPERATION!!");
        }

        [Runnable("ret", "Return a value")]
        public string ReturnTask()
        {
            return "Hi there from inside a task!";
        }
    }
}