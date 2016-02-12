using System;
using RunForrest.Core.Attributes;
using RunForrest.Services;

namespace RunForrest.ComplexTasks
{
    public interface IComplexTask
    {
        void SayHello();
    }

    public class ComplexTask : IComplexTask
    {
        private const string TaskName = "complextask";

        private readonly IService service;

        public ComplexTask(IService service)
        {
            this.service = service;
        }

        [Task(TaskName)]
        public void SayHello()
        {
            Console.WriteLine("Hi, {0}", service.Name);
        }
    }
} 