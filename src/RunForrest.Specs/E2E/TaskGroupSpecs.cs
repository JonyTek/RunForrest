using System.Linq;
using NUnit.Framework;

namespace RunForrest.Specs.E2E
{
    [TestFixture]
    public class TaskGroupSpecs
    {
        [Test]
        public void ShouldExecuteABasicTaskGroup()
        {
            const string groupName = "taskgroup";
            var instructions = new[] { groupName, "-g" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("taskgrouptaskone taskgrouptasktwo"));
        }

        [Test]
        public void ShouldExecuteABasicTaskGroupParallel()
        {
            const string groupName = "taskgroup";
            var instructions = new[] { groupName, "-g", "-p" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("taskgrouptaskone taskgrouptasktwo"));
        }

        [Test]
        public void ShouldExecuteABasicTaskGroupInPriorityOrder()
        {
            const string groupName = "taskgroup";
            var instructions = new[] { groupName, "-g" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Values, Is.EqualTo(new object[] { 0, 1, 2 }));
        }

        [Test]
        public void ShouldExecuteABasicTaskGroupInParallelWithNoPriorityOrder()
        {
            const string groupName = "taskgroup";
            var instructions = new[] { groupName, "-g", "-p" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Values.Last(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldExecuteABasicTaskGroupWithConstructorInjection()
        {
            const string groupName = "taskgroup";
            var instructions = new[] { groupName, "-g" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value1, Is.EqualTo("THIS IS MY STRING"));
        }

        [Test]
        public void ShouldExecuteABasicTaskGroupWithConstructorArguments()
        {
            const string groupName = "taskgroup1";
            var instructions = new[] { groupName, "-g", "-c", "jony" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("jony"));
        }

        [Test]
        public void ShouldExecuteABasicTaskGroupWithConstructorAndMethodArguments()
        {
            const string groupName = "taskgroup1";
            var instructions = new[] { groupName, "-g", "-c", "jony", "-m", "kay" };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("jony"));
            Assert.That(TestHelper.Value1, Is.EqualTo("kay"));
        }
    }
}