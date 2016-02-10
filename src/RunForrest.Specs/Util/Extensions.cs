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
            TaskCollection.Initialise<Tasks>();
            var signature = TaskCollection.Get("mytask").Signature;

            Assert.That(signature, Is.EqualTo("Void MyTask(String input)"));
        }
    }
}