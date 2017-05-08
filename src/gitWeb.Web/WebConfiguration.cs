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
        private static string _currentRepository = "currentRepository";
        private object syncObject = new object();

        public void SetRepository(string path)
        {
            WebConfigurationManager.AppSettings[_currentRepository] = path;

            var repositories = WebConfigurationManager.AppSettings[_repositoryPathSection].Split(';').ToList();

            if (repositories.Contains(path)) return;

            repositories.Add(path);

            WebConfigurationManager.AppSettings[_repositoryPathSection] = string.Join(";", repositories);
        }

        public string LoadPath()
        {
            lock (syncObject)
            {
                var currentRepository = WebConfigurationManager.AppSettings[_currentRepository];

                if (!string.IsNullOrEmpty(currentRepository)) return currentRepository;

                var repository = GetMappedRepositories().First();
                WebConfigurationManager.AppSettings[_currentRepository] = repository;
                return repository;
            }
        }

        private static string[] GetMappedRepositories()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection].Split(';');
        }


        public string[] LoadMappedRepositories()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection].Split(';');
        }
    }
}