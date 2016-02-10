using System;
using RunForrest.Core.Model;

namespace RunForrest.Tasks
{
    public class RunForrestTask
    {
        [Task("run")]
        public void RunTask()
        {
            Console.WriteLine("Run Forrest, run!");
        }

        [Task("dowhat")]
        public void DoWhatTask(string verb)
        {
            Console.WriteLine("Run Forrest, {0}!", verb);
        }

        [Task("ex", "Throw an exception")]
        public void ExceptionTask()
        {
            throw new Exception("INVALID OPERATION!!");
        }

        [Task("ret", "Return a value")]
        public string ReturnTask()
        {
            return "Hi there from inside a task!";
        }
    }
}