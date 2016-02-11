using System.Linq;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    internal class ConsoleInstructionParser : IParseInstructions
    {
        private readonly string[] arguments;

        internal ConsoleInstructionParser(string[] arguments)
        {
            this.arguments = arguments;
        }

        public InstructionSet ParseInstructions()
        {
            var index = 0;
            var set = new InstructionSet(InstructionsFrom.Console);

            if (arguments.Length == 0)
            {
                return set;
            }

            if (arguments[index].ToInstructionType() == InstructionType.None)
            {
                set.ExecuteAlias = arguments[index];
                index++;
            }

            for (; index < arguments.Length; index++)
            {
                if (arguments[index].IsASwitch())
                {
                    set.Instructions.Add(new Instruction
                    {
                        InstructionsFrom = InstructionsFrom.Console,
                        InstructionType = arguments[index].ToInstructionType(),
                        Arguments = arguments.Skip(index + 1).TakeWhile(x => !x.IsASwitch()),
                    });
                }
            }

            return set;
        }
    }
}