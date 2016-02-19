using NUnit.Framework;
using RunForrest.Core.Model;

namespace RunForrest.Specs.Model
{
    [TestFixture]
    public class AbstractTaskSpecs
    {
        [Test]
        public void ShouldPrintMethodInfo()
        {
            var signature = TaskCollection.SelectTask("basictaskmethodargs").MethodSignature;

            Assert.That(signature, Is.EqualTo("public Void Method(String value){ }"));
        }

        [Test]
        public void ShouldPrintMethodUsage()
        {
            var usage = TaskCollection.SelectTask("basictaskmethodargs").UsageExample;

            Assert.That(usage, Is.EqualTo("<appname> basictaskmethodargs -m <value> "));
        }
    }
}