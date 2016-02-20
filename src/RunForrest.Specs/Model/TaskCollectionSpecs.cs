using System.Linq;
using NUnit.Framework;
using RunForrest.Core.Model;

namespace RunForrest.Specs.Model
{
    [TestFixture]
    public class TaskCollectionSpecs
    {
        [Test]
        public void ShouldConfigureBasicTasks()
        {
            var task = TaskCollection.SelectTask("basictask");

            Assert.That(task.Method, Is.Not.Null);
            Assert.That(task.Priority, Is.EqualTo(10));
            Assert.That(task.Description, Is.EqualTo("basic task description"));
        }

        [Test]
        public void ShouldConfigureTaskGroups()
        {
            var group = TaskCollection.SelectTaskGroup("taskgroup");
            var tasks = group.Tasks.ToArray();

            Assert.That(tasks.Length, Is.GreaterThan(2));
            Assert.That(group.Description, Is.EqualTo("task group description"));
        }
    }
}