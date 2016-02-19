using System.Linq;
using System.Reflection;
using NUnit.Framework;
using RunForrest.Core.Attributes;
using RunForrest.Core.Util;

namespace RunForrest.Specs.Util
{
    [TestFixture]
    public class ReflectionExtensionsSpecs
    {
        private readonly MethodInfo method = typeof (TaskGroup).GetMethodByName("mytask");

        [Test]
        public void ShouldGetMethodByName()
        {
            var type = typeof (TaskGroup);
            var method = type.GetMethodByName("mytask");

            Assert.That(method, Is.Not.Null);
        }

        [Test]
        public void ShouldGetTaskPriority()
        {
            var priority = method.GetTaskPriority();

            Assert.That(priority, Is.EqualTo(10));
        }

        [Test]
        public void ShouldGetTaskAlias()
        {
            var alias = method.GetTaskAlias();

            Assert.That(alias, Is.EqualTo("mytask"));
        }

        [Test]
        public void ShouldGetTaskDesc()
        {
            var desc = method.GetTaskGroupDescription();

            Assert.That(desc, Is.EqualTo("mytaskdesc"));
        }

        [Test]
        public void ShouldGetTaskGroupAlias()
        {
            var alias = typeof(TaskGroup).GetTaskGroupAlias();

            Assert.That(alias, Is.EqualTo("groupname"));
        }

        [Test]
        public void ShouldGetTaskGroupDesc()
        {
            var desc = typeof(TaskGroup).GetTaskGroupDescription();

            Assert.That(desc, Is.EqualTo("groupdesc"));
        }

        [Test]
        public void ShouldScanForSingleTasks()
        {
            var tasks = typeof(TaskGroup).Assembly.ScanForSingleTasks().ToArray();

            Assert.That(tasks.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ShouldScanForTaskGroups()
        {
            var groups = typeof(TaskGroup).Assembly.ScanForTaskGroups().ToArray();

            Assert.That(groups.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ShouldCheckIfIsTask()
        {
            var isTask = method.IsTask();

            Assert.That(isTask, Is.EqualTo(true));
        }

        [Test]
        public void ShouldCheckIfIsTaskGroup()
        {
            var isGroup = typeof (TaskGroup).IsTaskGroup();

            Assert.That(isGroup, Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetConfigurations()
        {
            var configs = typeof(TaskGroup).Assembly.GetRunForrestConfigurations().ToArray();

            Assert.That(configs.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ShouldGetComplexTaskConfigurations()
        {
            var configs = typeof(TaskGroup).Assembly.GetComplexTaskConfigurations().ToArray();

            Assert.That(configs.Length, Is.GreaterThan(0));
        }
    }

    [TaskGroup("groupname", "groupdesc")]
    public class TaskGroup
    {
        private const string TaskName = "mytask";

        private const string TaskDesc = "mytaskdesc";

        [Task(TaskName, TaskDesc, 10)]
        public void MyTask()
        {
        }
    }
}