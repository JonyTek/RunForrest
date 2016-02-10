using RunForrest.Core.Model;

namespace RunForrest.Core.Runners
{
    internal interface IRunInstructions
    {
        void Run(ApplicationInstructions instructions);
    }
}