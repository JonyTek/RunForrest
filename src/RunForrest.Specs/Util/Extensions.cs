using System.Reflection;
using NUnit.Framework;
using RunForrest.Core.Model;

namespace RunForrest.Specs.Util
{
    [TestFixture]
    public class Extensions
    {
        [Test]
        public void ShouldPrintMethodInfo()
        {
            TaskCollection.Initialise<Tasks>(RunForrestConfiguration.Instance);

            var signature = TaskCollection.Select("mytask").Signature;

            Assert.That(signature, Is.EqualTo("Void MyTask(String input)"));
        }
    }
}