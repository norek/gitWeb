using gitWeb.Core.Features.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace gitWeb.Web
{
    public class WebConfig : IConfigurationRepository
    {
        private static string _repositoryPathSection = "repositoryPath";

        public void SetRepository(string path)
        {
            var repositories = WebConfigurationManager.AppSettings[_repositoryPathSection].Split(';').ToList();

            if (repositories.Contains(path)) return;

            repositories.Add(path);

            WebConfigurationManager.AppSettings[_repositoryPathSection] = string.Join(";", repositories);
        }

        public string LoadPath()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection].Split(';').FirstOrDefault();
        }


        public string[] LoadMappedRepositories()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection].Split(';');
        }
    }
}