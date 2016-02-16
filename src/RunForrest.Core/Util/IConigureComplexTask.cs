using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public interface IConigureComplexTask<TInstance> : IConigureComplexTask
    {
        void Setup(ComplexTaskConfiguration<TInstance> configuration);
    }

    public interface IConigureComplexTask
    {
    }
}