using System.Web.Configuration;
using gitWeb.Core.Features.Configuration;

namespace gitWeb
{
    public class WebConfig : IConfigurationRepository
    {
        private static string _repositoryPathSection = "repositoryPath";

        public void SavePath(string path)
        {
            WebConfigurationManager.AppSettings[_repositoryPathSection] = path;
        }

        public string LoadPath()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection];
        }
    }
}