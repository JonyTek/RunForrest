using NUnit.Framework;

namespace RunForrest.Specs.E2E
{
    [TestFixture]
    public class ComplexTaskSpecs
    {
        [Test]
        public void ShouldExecuteAComplexTaskAsAnInterface()
        {
            const string taskName = "complextaskasinterface";
            var instructions = new[] { taskName };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("THIS IS MY STRING"));
        }

        [Test]
        public void ShouldExecuteAComplexTaskAsAnClass()
        {
            const string taskName = "complextaskasclass";
            var instructions = new[] { taskName };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo("THIS IS MY STRING"));
        }

        [Test]
        public void ShouldExecuteAComplexTaskWithMethodArgs()
        {
            const string taskName = "complextaskwithmethodarg";
            var instructions = new[] { taskName, "-m", taskName };
            TestHelper.GenerateInstructions(instructions, true).Run();

            Assert.That(TestHelper.Value, Is.EqualTo(taskName));
        }
    }
}