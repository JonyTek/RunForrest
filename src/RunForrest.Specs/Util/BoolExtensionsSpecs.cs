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
            const bool value = false;
            value.ExecuteIfTrue(Assert.Fail);
        }

        [Test]
        public void ShouldExecuteIfFalse()
        {
            const bool value = true;
            value.ExecuteIfFalse(Assert.Fail);
        }
    }
}