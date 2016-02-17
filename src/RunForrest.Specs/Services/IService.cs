namespace RunForrest.Specs.Services
{
    public interface IService
    {
        string GetString();
    }

    public class Service : IService
    {
        private readonly IRepository repository;

        public Service(IRepository repository)
        {
            this.repository = repository;
        }

        public string GetString()
        {
            return repository.GetString().ToUpper();
        }
    }
}