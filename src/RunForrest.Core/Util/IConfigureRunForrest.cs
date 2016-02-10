using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public interface IConfigureRunForrest<TConfiguration> : IConfigureRunForrest
        where TConfiguration : new()
    {
    }

    public interface IConfigureRunForrest
    {
        void Configure(RunForrestConfiguration configuration);
    }
}