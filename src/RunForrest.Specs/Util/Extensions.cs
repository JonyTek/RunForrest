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
            TaskCollection.Initialise<Tasks>(new ApplicationConfiguration());

            var signature = TaskCollection.SelectTask("mytask").MethodSignature;

            Assert.That(signature, Is.EqualTo("public Void MyTask(String input){ }"));
        }
    }
}