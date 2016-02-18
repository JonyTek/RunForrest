﻿using System;
using System.Runtime.Remoting.Messaging;
using RunForrest.Core.Model;
using RunForrest.Core.Util;
using RunForrest.Specs.Services;

namespace RunForrest.Specs.Tasks
{
    public interface IComplexTask
    {
        void DoSomething();
    }

    public class ComplexTask : IComplexTask
    {
        private readonly IService service;

        public ComplexTask(IService service)
        {
            this.service = service;
        }

        public void DoSomething()
        {
            TestHelper.Value = service.GetString();
        }
    }

    public class ComplexTaskConfigurationAsInterface : IConfigureComplexTask<IComplexTask>
    {
        public void Setup(ComplexTaskConfiguration<IComplexTask> configuration)
        {
            configuration.WithAlias("complextaskasinterface");
            configuration.WithDescription("this is my description");
            configuration.OnMethodWithName("dosomething");

            configuration.WithOnAfterTask((task, ret) => { Console.WriteLine(ret); });
        }
    }

    public class ComplexTaskConfigurationAsClass : IConfigureComplexTask<ComplexTask>
    {
        public void Setup(ComplexTaskConfiguration<ComplexTask> configuration)
        {
            configuration.WithAlias("complextask1");
            configuration.OnMethodWithName("dosomething");
        }
    }
}