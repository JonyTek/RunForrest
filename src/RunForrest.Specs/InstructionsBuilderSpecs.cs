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
            var instructions = UserInputParser.Parse(input);

            Assert.That(instructions.Runner is ExecuteListInstructions, Is.True);
        }

        [Test]
        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedTaskAlias()
        {
            var input = new[] { "alias", "-l" };
            var instructions = UserInputParser.Parse(input);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions));
        }

        [Test]
        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
        {
            var input = new[] { "-l", "-v" };
            var instructions = UserInputParser.Parse(input);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions));
        }

        [Test]
        public void ShouldSetHelpInstructions()
        {
            var input = new[] { "-h" };
            var instructions = UserInputParser.Parse(input);

            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetHelpInstructionsIfNoArgumentsPassed()
        {
            var input = new string[0];
            var instructions = UserInputParser.Parse(input);

            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
        }

        [Test]
        public void ShouldSetHelpInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
        {
            var input = new[] { "-h", "-v" };
            var instructions = UserInputParser.Parse(input);

            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions));
        }

        [Test]
        public void ShouldSetTaskInstructions()
        {
            var input = new[] {"alias", "-m", "method1", "method2", "-c", "input1", "input2", "-v", "-t"};
            var instructions = UserInputParser.Parse(input);

            Assert.That(instructions.Runner is ExecuteTaskInstructions, Is.True);
            Assert.That(instructions.Instructions[SwitchType.Timed], Is.EquivalentTo(new List<string>()));
            Assert.That(instructions.Instructions[SwitchType.Verbose], Is.EquivalentTo(new List<string>()));
            Assert.That(instructions.Instructions[SwitchType.Method], Is.EquivalentTo(new[] { "method1", "method2" }));
            Assert.That(instructions.Instructions[SwitchType.Constructor], Is.EquivalentTo(new[] { "input1", "input2" }));
        }

        [Test]
        public void ShouldThrowIfArgumentsPassedWithoutArgumentSwitch()
        {
            var input = new[] { "alias", "invalid" };

            Assert.Throws<ArgumentException>(() => UserInputParser.Parse(input));
        }
    }
}