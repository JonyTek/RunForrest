using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal class InstructionBuilder
    {
        private readonly InstructionSet consoleInstructions;

        private readonly InstructionSet configInstructions;

        private readonly RunForrestConfiguration configuration;

        internal InstructionBuilder(string[] arguments, RunForrestConfiguration configuration)
        {
            this.configuration = configuration;

            consoleInstructions = new ConsoleInstructionParser(arguments).ParseInstructions();
            configInstructions = new ConfigurationInstructionParser(configuration).ParseInstructions();
        }

        private void UpdateFor(InstructionType instructionType, ApplicationInstructions instructions)
        {
            consoleInstructions.DoesntContainsInstruction(instructionType)
                .ExecuteIfTrue(
                    () =>
                        configInstructions.ContainsInstruction(instructionType)
                            .ExecuteIfTrue(
                                () =>
                                    instructions.SetInstructions(instructionType,
                                        configInstructions.ByType(instructionType))));
        }

        internal ApplicationInstructions Build()
        {
            var instructions = consoleInstructions.ToApplicationInstructions(configuration, InstructionsFrom.Console);

            string.IsNullOrEmpty(consoleInstructions.ExecuteAlias)
                .ExecuteIfTrue(
                    () =>
                        instructions.ExecuteAlias =
                            new ExecuteAlias
                            {
                                Alias = configuration.ExecuteAlias,
                                InstructionsFrom = InstructionsFrom.Configuration
                            });

            UpdateFor(InstructionType.DisplayHelp, instructions);
            UpdateFor(InstructionType.DisplayList, instructions);
            UpdateFor(InstructionType.Constructor, instructions);
            UpdateFor(InstructionType.Method, instructions);
            UpdateFor(InstructionType.Group, instructions);
            UpdateFor(InstructionType.Timed, instructions);
            UpdateFor(InstructionType.Verbose, instructions);

            return instructions;
        }
    }
}