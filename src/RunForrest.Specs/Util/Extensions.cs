using System.Diagnostics;
using System.Linq;
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
            var config = ApplicationConfiguration.ConfigureApp<Tasks>();
            TaskCollection.Initialise<Tasks>(config);

            var signature = TaskCollection.SelectTask("mytask").MethodSignature;

            Assert.That(signature, Is.EqualTo("public Void MyTask(String input){ }"));
        }
    }
}