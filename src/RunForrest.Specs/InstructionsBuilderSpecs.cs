using System;
using System.Collections.Generic;
using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Core.Runners;
using RunForrest.Core.Util;

namespace RunForrest.Specs
{
    [TestFixture]
    public class InstructionsBuilderSpecs
    {
        [SetUp]
        public void OnBeforeEachTest()
        {
        }

        [Test]
        public void ShouldSetListInstructions()
        {
            var input = new[] { "-l" };
            var instructions = InstructionBuilder.Build(input);

            Assert.That(instructions.Runner is RunListInstructions, Is.True);
        }

        [Test]
        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedTaskAlias()
        {
            var input = new[] { "alias", "-l" };
            var instructions = InstructionBuilder.Build(input);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Run(instructions));
        }

        [Test]
        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
        {
            var input = new[] { "-l", "-v" };
            var instructions = InstructionBuilder.Build(input);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Run(instructions));
        }

        [Test]
        public void ShouldSetHelpInstructions()
        {
            var input = new[] { "-h" };
            var instructions = InstructionBuilder.Build(input);

            Assert.That(instructions.Runner is RunHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetHelpInstructionsIfNoArgumentsPassed()
        {
            var input = new string[0];
            var instructions = InstructionBuilder.Build(input);

            Assert.That(instructions.Runner is RunHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetHelpInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
        {
            var input = new[] { "-h", "-v" };
            var instructions = InstructionBuilder.Build(input);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Run(instructions));
        }

        [Test]
        public void ShouldSetTaskInstructions()
        {
            var input = new[] {"alias", "-m", "method1", "method2", "-c", "input1", "input2", "-v", "-t"};
            var instructions = InstructionBuilder.Build(input);

            Assert.That(instructions.Runner is RunTaskInstructions, Is.True);
            Assert.That(instructions.Instructions[SwitchType.Timed], Is.EquivalentTo(new List<string>()));
            Assert.That(instructions.Instructions[SwitchType.Verbose], Is.EquivalentTo(new List<string>()));
            Assert.That(instructions.Instructions[SwitchType.Method], Is.EquivalentTo(new[] { "method1", "method2" }));
            Assert.That(instructions.Instructions[SwitchType.Constructor], Is.EquivalentTo(new[] { "input1", "input2" }));
        }

        [Test]
        public void ShouldThrowIfArgumentsPassedWithoutArgumentSwitch()
        {
            var input = new[] { "alias", "invalid" };

            Assert.Throws<ArgumentException>(() => InstructionBuilder.Build(input));
        }
    }
}