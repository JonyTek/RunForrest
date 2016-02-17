using System;
using RunForrest.Services;

namespace RunForrest.ComplexTasks
{
    public interface IComplexTask
    {
        void SayHello(string yo);
    }

    public class MyComplexTask : IComplexTask
    {
        private readonly IService service;

        public MyComplexTask(IService service)
        {
            this.service = service;
        }

        public void SayHello(string yo)
        {
            Console.WriteLine(yo);

            Console.WriteLine("Hi, {0}", service.Name);
        }
    }
} 