using System.Collections.ObjectModel;
using System.Linq;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal class InstructionSet
    {
        internal InstructionSet(InstructionsFrom instructionsFrom)
        {
            ExecuteAlias = string.Empty;
            InstructionsFrom = instructionsFrom;
            Instructions = new Collection<Instruction>();
        }

        internal string ExecuteAlias { get; set; }

        internal InstructionsFrom InstructionsFrom { get; set; }

        internal Collection<Instruction> Instructions { get; }

        internal Instruction ByType(InstructionType switchType)
        {
            return Instructions.FirstOrDefault(x => x.InstructionType == switchType);
        }

        internal bool ContainsInstruction(InstructionType switchType)
        {
            return ByType(switchType) != null;
        }

        internal bool DoesntContainsInstruction(InstructionType switchType)
        {
            return ByType(switchType) == null;
        }

        internal ApplicationInstructions ToApplicationInstructions(ApplicationConfiguration configuration,
            InstructionsFrom instructionsFrom)
        {
            var appInstructions = new ApplicationInstructions(configuration)
            {
                ExecuteAlias = new ExecuteAlias {Alias = ExecuteAlias, InstructionsFrom = instructionsFrom}
            };

            foreach (var instruction in Instructions)
            {
                appInstructions.Instructions.Add(instruction.InstructionType, instruction);
            }

            return appInstructions;
        }
    }
}