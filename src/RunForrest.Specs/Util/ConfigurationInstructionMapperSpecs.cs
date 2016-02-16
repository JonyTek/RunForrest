using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Specs.Util
{
    [TestFixture]
    public class ConfigurationInstructionMapperSpecs
    {
        private ConfigurationInstructionMapper mapper;

        [Test]
        public void ShouldSetValuesWhenSet()
        {
            var config = new ApplicationConfiguration
            {
                IsInGroupMode = true,
                IsTimedMode = true,
                IsVerbodeMode = true
            };
            config.DefaultArguments.ExecuteAlias = "alias";
            config.DefaultArguments.MethodArguments = new[] { "method" };
            config.DefaultArguments.ConstructorArguments = new[] { "ctor" };

            mapper = new ConfigurationInstructionMapper(config);
            var instructionSet = mapper.MapInstructions();

            Assert.That(instructionSet.ExecuteAlias, Is.EqualTo("alias"));
            Assert.That(instructionSet.ByType(InstructionType.Group), Is.Not.Null);
            Assert.That(instructionSet.ByType(InstructionType.Timed), Is.Not.Null);
            Assert.That(instructionSet.ByType(InstructionType.Verbose), Is.Not.Null);
            Assert.That(instructionSet.ByType(InstructionType.Method).Arguments, Is.EquivalentTo(new[] { "method" }));
            Assert.That(instructionSet.ByType(InstructionType.Constructor).Arguments, Is.EquivalentTo(new[] { "ctor" }));
        }

        [Test]
        public void ShouldNotSetValuesWhenNotSet()
        {
            var config = new ApplicationConfiguration();
            mapper = new ConfigurationInstructionMapper(config);
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