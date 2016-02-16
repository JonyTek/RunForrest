using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Core.Util;

namespace RunForrest.Specs.Util
{
    public class InstructionBuilderSpecs
    {
        private InstructionBuilder builder;

        [Test]
        public void ShouldMapAllConsoleFields()
        {
            var console = new[] { "alias", "-m", "console", "-c", "console", "-v", "-t", "-g", "-h", "-l" };

            builder = new InstructionBuilder(console, new ApplicationConfiguration());
            var instructions = builder.Build();

            Assert.That(instructions.ExecuteAlias.Alias, Is.EqualTo("alias"));
            Assert.That(instructions.Instructions[InstructionType.DisplayHelp], Is.Not.Null);
            Assert.That(instructions.Instructions[InstructionType.DisplayList], Is.Not.Null);
            Assert.That(instructions.Instructions[InstructionType.Constructor].Arguments, Is.EqualTo(new[] { "console" }));
            Assert.That(instructions.Instructions[InstructionType.Method].Arguments, Is.EqualTo(new[] { "console" }));
            Assert.That(instructions.Instructions[InstructionType.Group], Is.Not.Null);
            Assert.That(instructions.Instructions[InstructionType.Timed], Is.Not.Null);
            Assert.That(instructions.Instructions[InstructionType.Verbose], Is.Not.Null);
        }

        [Test]
        public void ShouldMapAllConfigFields()
        {
            var console = new string[0];
            var config = new ApplicationConfiguration
            {
                IsInGroupMode = true,
                IsTimedMode = true,
                IsVerbodeMode = true
            };
            config.DefaultArguments.ExecuteAlias = "alias";
            config.DefaultArguments.MethodArguments = new[] { "config" };
            config.DefaultArguments.ConstructorArguments = new[] { "config" };

            builder = new InstructionBuilder(console, config);
            var instructions = builder.Build();

            Assert.That(instructions.ExecuteAlias.Alias, Is.EqualTo("alias"));
            Assert.That(instructions.Instructions.ContainsKey(InstructionType.DisplayHelp), Is.False);
            Assert.That(instructions.Instructions.ContainsKey(InstructionType.DisplayList), Is.False);
            Assert.That(instructions.Instructions[InstructionType.Constructor].Arguments, Is.EqualTo(new[] { "config" }));
            Assert.That(instructions.Instructions[InstructionType.Method].Arguments, Is.EqualTo(new[] { "config" }));
            Assert.That(instructions.Instructions[InstructionType.Group], Is.Not.Null);
            Assert.That(instructions.Instructions[InstructionType.Timed], Is.Not.Null);
            Assert.That(instructions.Instructions[InstructionType.Verbose], Is.Not.Null);
        }
    }
}