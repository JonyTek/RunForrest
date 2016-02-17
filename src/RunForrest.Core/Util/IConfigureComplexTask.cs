using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public interface IConfigureComplexTask<TInstance> : IConfigureComplexTask
    {
        void Setup(ComplexTaskConfiguration<TInstance> configuration);
    }

    public interface IConfigureComplexTask
    {
    }
}