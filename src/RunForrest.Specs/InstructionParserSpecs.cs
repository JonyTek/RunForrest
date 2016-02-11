using System;
using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Core.Runners;
using RunForrest.Core.Util;
using Assert = NUnit.Framework.Assert;

namespace RunForrest.Specs
{
    [TestFixture]
    public class InstructionParserSpecs
    {
        [SetUp]
        public void OnBeforeEachTest()
        {
        }

        [Test]
        public void ShouldSetListInstructions()
        {
            var input = new[] { "-l" };
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.That(instructions.Runner is ExecuteListInstructions, Is.True);
        }

        [Test]
        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedTaskAlias()
        {
            var input = new[] { "alias", "-l" };
            var config = new RunForrestConfiguration();
            var instructions = InstructionParser.ParseInstructions(input, config);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions, config));
        }

        [Test]
        public void ShouldSetDisplayWhenCalledIfSpecifiedTaskAlias()
        {
            var input = new[] { "alias", "-h" };
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
        {
            var input = new[] { "-l", "-v" };
            var config = new RunForrestConfiguration();
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions, config));
        }

        [Test]
        public void ShouldSetHelpInstructions()
        {
            var input = new[] { "-h" };
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetHelpInstructionsIfNoArgumentsPassed()
        {
            var input = new string[0];
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetHelpInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
        {
            var input = new[] { "-h", "-v" };
            var config = new RunForrestConfiguration();
            var instructions = InstructionParser.ParseInstructions(input, config);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions, config));
        }

        [Test]
        public void ShouldSetTaskInstructions()
        {
            var input = new[] {"alias", "-m", "method1", "method2", "-c", "input1", "input2", "-v", "-t"};
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.That(instructions.Runner is ExecuteSingleTaskInstructions, Is.True);
            Assert.That(instructions.TimedMode, Is.True);
            Assert.That(instructions.VerbodeMode, Is.True);
            Assert.That(instructions.Instructions[SwitchType.Method], Is.EquivalentTo(new[] { "method1", "method2" }));
            Assert.That(instructions.Instructions[SwitchType.Constructor], Is.EquivalentTo(new[] { "input1", "input2" }));
        }

        [Test]
        public void ShouldThrowIfArgumentsPassedWithoutArgumentSwitch()
        {
            var input = new[] { "alias", "invalid" };

            Assert.Throws<ArgumentException>(() => InstructionParser.ParseInstructions(input, new RunForrestConfiguration()));
        }


        [Test]
        public void ShouldSetTaskGroupInstructions()
        {
            var input = new[] {"alias", "-g"};
            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

            Assert.That(instructions.Runner is ExecuteGroupTaskInstructions, Is.True);
        }
    }
}