using NUnit.Framework;
using RunForrest.Core.Model;

namespace RunForrest.Specs.Model
{
    [TestFixture]
    public class ApplicationInstructionsSpecs
    {
        [Test]
        public void ShouldSetTheApplicationModeToHelpIfNoAlias()
        {
            var instructions = new ApplicationInstructions(null);
            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
        }

        [Test]
        public void ShouldSetTheApplicationModeToHelpIfInstructionsContainsHelp()
        {
            var instructions = new ApplicationInstructions(null);
            instructions.Instructions.Add(InstructionType.DisplayHelp, new Instruction());

            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
        }

        [Test]
        public void ShouldSetTheApplicationModeToListIfInstructionsContainsListAndNoAlias()
        {
            var instructions = new ApplicationInstructions(null);
            instructions.Instructions.Add(InstructionType.DisplayList, new Instruction());

            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.List));
        }

        [Test]
        public void ShouldSetTheApplicationModeToGroupIfInstructionsContainsGroup()
        {
            var instructions = new ApplicationInstructions(null)
            {
                ExecuteAlias = {Alias = "alias"}
            };
            instructions.Instructions.Add(InstructionType.Group, new Instruction());

            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Group));
        }

        [Test]
        public void ShouldSetTheApplicationModeToHelpIfInstructionsContainsGroup()
        {
            var instructions = new ApplicationInstructions(null)
            {
                ExecuteAlias = { Alias = "alias" }
            };

            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Single));
        }
    }
}