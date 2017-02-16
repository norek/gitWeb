using System;
using System.Web.Configuration;

namespace gitWeb
{
    public class GitConfigurationProvider
    {
        private readonly RepositoryPathValdiator _repositoryPathValdiator;
        private static string _repositoryPathSection = "repositoryPath";

        public GitConfigurationProvider(RepositoryPathValdiator repositoryPathValdiator)
        {
            _repositoryPathValdiator = repositoryPathValdiator;
        }

        public bool SaveRepositoryPath(string path)
        {
            if (_repositoryPathValdiator.IsInvalid(path))
            {
                return false;
            }

            WebConfigurationManager.AppSettings[_repositoryPathSection] = path;

            return true;
        }

        public string LoadRepositoryPath()
        {
            return WebConfigurationManager.AppSettings[_repositoryPathSection];
        }
    }
}