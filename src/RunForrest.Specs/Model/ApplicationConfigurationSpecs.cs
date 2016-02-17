using System;
using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Specs.Tasks;

namespace RunForrest.Specs.Model
{
    [TestFixture]
    public class ApplicationConfigurationSpecs
    {
        private ApplicationConfiguration config;

        [SetUp]
        public void OnBeforeEachTest()
        {
            TestHelper.Bootstrap();
            config = TestHelper.Config;
        }

        [Test]
        public void ShouldConfigureAppWithConfigClass()
        {
            Assert.That(config.ConsoleColor, Is.EqualTo(ConsoleColor.DarkRed));
            //Assert.That(config.IsInGroupMode, Is.True);
            Assert.That(config.IsTimedMode, Is.True);
            Assert.That(config.IsVerbodeMode, Is.True);
        }

        [Test]
        public void ShouldConfigureComplexTasks()
        {
            var task = TaskCollection.SelectTask("complextaskasinterface");

            Assert.That(task.Method, Is.Not.Null);
            Assert.That(task.Description, Is.EqualTo("this is my description"));
        }

        [Test]
        public void ShouldConfigureIoc()
        {
            var task = config.Ioc.Resolve(typeof (ComplexTask)) as IComplexTask;
            task.DoSomething();

            Assert.That(TestHelper.Value, Is.EqualTo("this is my string".ToUpper()));
        }
    }
}