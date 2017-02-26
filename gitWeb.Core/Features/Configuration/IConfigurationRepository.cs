namespace gitWeb.Core.Features.Configuration
{
    public interface IConfigurationRepository
    {
        void SavePath(string path);
        string LoadPath();
    }
}