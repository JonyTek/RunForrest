using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public interface IConfigureRunForrest
    {
        void Setup(RunForrestConfiguration configuration);
    }
}