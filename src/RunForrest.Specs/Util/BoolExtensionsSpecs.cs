using NUnit.Framework;
using RunForrest.Core.Util;

namespace RunForrest.Specs.Util
{
    [TestFixture]
    public class BoolExtensionsSpecs
    {
        [Test]
        public void ShouldExecuteIfTrue()
        {
            const bool value = true;
            value.ExecuteIfTrue(Assert.Pass);

            Assert.Fail();
        }

        [Test]
        public void ShouldExecuteIfFalse()
        {
            const bool value = false;
            value.ExecuteIfFalse(Assert.Pass);

            Assert.Fail();
        }
    }
}