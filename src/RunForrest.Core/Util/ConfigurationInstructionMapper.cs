using System.Linq;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal class ConfigurationInstructionMapper : IMapInstructions
    {
        private readonly ApplicationConfiguration configuration;

        internal ConfigurationInstructionMapper(ApplicationConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public InstructionSet MapInstructions()
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
                        Arguments = configuration.DefaultArguments.ConstructorArguments,
                        InstructionsFrom = InstructionsFrom.Configuration
                    }));
            configuration.DefaultArguments.MethodArguments.Any()
                .ExecuteIfTrue(() => set.Instructions.Add(
                    new Instruction
                    {
                        InstructionType = InstructionType.Method,
                        Arguments = configuration.DefaultArguments.MethodArguments,
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