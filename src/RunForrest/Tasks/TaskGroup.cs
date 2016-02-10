using System;
using RunForrest.Core.Attributes;

namespace RunForrest.Tasks
{
    [TaskGroup("group")]
    public class TaskGroup
    {
        [Task("groupone", priority: 1)]
        public void One()
        {
            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("1 priority 1");
        }

        [Task("grouptwo", priority: 2)]
        public void Two()
        {
            Console.WriteLine("1 priority 2");
        }

        [Task("groupthree", priority: 0)]
        public void Three()
        {
            Console.WriteLine("3 priority 0");
        }
    }
}