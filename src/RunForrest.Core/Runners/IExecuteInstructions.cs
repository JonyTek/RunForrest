using RunForrest.Core.Model;

namespace RunForrest.Core.Runners
{
    internal interface IExecuteInstructions
    {
        void Execute(UserInput instructions);
    }
}