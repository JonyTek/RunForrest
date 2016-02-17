using NUnit.Framework;
using RunForrest.Core.Attributes;
using RunForrest.Specs.Services;

namespace RunForrest.Specs.E2E
{
    [TestFixture]
    public class BasicTaskSpecs
    {
        [Test]
        public void ShouldExecuteABasicTaskWithNoArgs()
        {
            const string taskName = "basictasknoargs";
            var instructions = new[] { taskName};
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo(taskName));
        }

        [Test]
        public void ShouldExecuteABasicTaskWithMethodArgs()
        {
            const string taskName = "basictaskmethodargs";
            var instructions = new[] { taskName, "-m", taskName };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo(taskName));
        }

        [Test]
        public void ShouldExecuteABasicTaskWithCtorArgs()
        {
            const string taskName = "basictaskctorargs";
            var instructions = new[] { taskName, "-c", taskName };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo(taskName));
        }

        [Test]
        public void ShouldExecuteABasicTaskWithCtorInjection()
        {
            const string taskName = "basictaskctorinjection";
            var instructions = new[] { taskName };
            TestHelper.GenerateInstructions(instructions).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("THIS IS MY STRING"));
        }
    }

    public class BasicTaskNoArgs
    {
        private const string TaskName = "basictasknoargs";

        [Task(TaskName)]
        public void Method()
        {
            TestHelper.Value = TaskName;
        }
    }

    public class BasicTaskMethodArgs
    {
        private const string TaskName = "basictaskmethodargs";

        [Task(TaskName)]
        public void Method(string value)
        {
            TestHelper.Value = value;
        }
    }

    public class BasicTaskCtorArgs
    {
        private const string TaskName = "basictaskctorargs";

        private readonly string value;

        public BasicTaskCtorArgs(string value)
        {
            this.value = value;
        }

        [Task(TaskName)]
        public void Method()
        {
            TestHelper.Value = value;
        }
    }

    public class BasicTaskCtorInjection
    {
        private readonly IService service;

        private const string TaskName = "basictaskctorinjection";

        public BasicTaskCtorInjection(IService service)
        {
            this.service = service;
        }

        [Task(TaskName)]
        public void Method()
        {
            TestHelper.Value = service.GetString();
        }
    }
}