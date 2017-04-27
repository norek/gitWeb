namespace gitWeb.Core.Features.Configuration
{
    public interface IConfigurationRepository
    {
        void SetRepository(string path);
        string LoadPath();

        string[] LoadMappedRepositories();
    }
}