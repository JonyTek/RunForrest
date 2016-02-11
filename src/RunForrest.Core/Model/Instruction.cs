using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RunForrest.Core.Model
{
    internal class Instruction
    {
        internal Instruction()
        {
            Arguments = new Collection<string>();
        }

        internal InstructionType InstructionType { get; set; }

        internal IEnumerable<string> Arguments { get; set; }

        internal InstructionsFrom InstructionsFrom { get; set; }
    }
}