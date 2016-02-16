using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Specs.Util
{
    public class ConsoleInstructionMapperSpecs
    {
        private IMapInstructions mapper;

        [Test]
        public void ShouldSetValuesWhenSet()
        {
            var input = new[] {"alias", "-m", "method", "method1", "-c", "ctor", "ctor1", "-v", "-t", "-g"};

            mapper = new ConsoleInstructionMapper(input);
            var instructionSet = mapper.MapInstructions();

            Assert.That(instructionSet.ExecuteAlias, Is.EqualTo("alias"));
            Assert.That(instructionSet.ByType(InstructionType.Group), Is.Not.Null);
            Assert.That(instructionSet.ByType(InstructionType.Timed), Is.Not.Null);
            Assert.That(instructionSet.ByType(InstructionType.Verbose), Is.Not.Null);
            Assert.That(instructionSet.ByType(InstructionType.Method).Arguments, Is.EquivalentTo(new[] { "method", "method1" }));
            Assert.That(instructionSet.ByType(InstructionType.Constructor).Arguments, Is.EquivalentTo(new[] { "ctor", "ctor1" }));
        }

        [Test]
        public void ShouldNotSetValuesWhenNotSet()
        {
            mapper = new ConsoleInstructionMapper(new string[0]);
            var instructionSet = mapper.MapInstructions();

            Assert.That(instructionSet.ExecuteAlias, Is.EqualTo(string.Empty));
            Assert.That(instructionSet.ByType(InstructionType.Group), Is.Null);
            Assert.That(instructionSet.ByType(InstructionType.Timed), Is.Null);
            Assert.That(instructionSet.ByType(InstructionType.Verbose), Is.Null);
            Assert.That(instructionSet.ByType(InstructionType.Method), Is.Null);
            Assert.That(instructionSet.ByType(InstructionType.Constructor), Is.Null);
        }
    }
}