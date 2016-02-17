namespace RunForrest.Specs.Services
{
    public interface IRepository
    {
        string GetString();
    }

    public class Repository : IRepository
    {
        public string GetString()
        {
            return "this is my string";
        }
    }
}