namespace RunForrest.Services
{
    public interface IService
    {
        string Name { get; }
    }

    public class Service : IService
    {
        public string Name => "Jony";
    }
}