using System.Linq;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal class ConfigurationInstructionMapper : IParseInstructions
    {
        private readonly ApplicationConfiguration configuration;

        internal ConfigurationInstructionMapper(ApplicationConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public InstructionSet ParseInstructions()
        {
            var set = new InstructionSet(InstructionsFrom.Configuration)
            {
                ExecuteAlias = configuration.DefaultArguments.ExecuteAlias
            };

            configuration.DefaultArguments.ConstructorArguments.Any()
                .ExecuteIfTrue(() => set.Instructions.Add(
                    new Instruction
                    {
                        InstructionType = InstructionType.Constructor,
                        Arguments = new string[0],
                        InstructionsFrom = InstructionsFrom.Configuration
                    }));
            configuration.DefaultArguments.MethodArguments.Any()
                .ExecuteIfTrue(() => set.Instructions.Add(
                    new Instruction
                    {
                        InstructionType = InstructionType.Method,
                        Arguments = new string[0],
                        InstructionsFrom = InstructionsFrom.Configuration
                    }));
            configuration.IsInGroupMode
                .ExecuteIfTrue(() => set.Instructions.Add(
                    new Instruction
                    {
                        InstructionType = InstructionType.Group,
                        Arguments = new string[0],
                        InstructionsFrom = InstructionsFrom.Configuration
                    }));
            configuration.IsTimedMode
                .ExecuteIfTrue(() => set.Instructions.Add(
                    new Instruction
                    {
                        InstructionType = InstructionType.Timed,
                        Arguments = new string[0],
                        InstructionsFrom = InstructionsFrom.Configuration
                    }));
            configuration.IsVerbodeMode
                .ExecuteIfTrue(() => set.Instructions.Add(
                    new Instruction
                    {
                        InstructionType = InstructionType.Verbose,
                        Arguments = new string[0],
                        InstructionsFrom = InstructionsFrom.Configuration
                    }));

            return set;
        }
    }
}