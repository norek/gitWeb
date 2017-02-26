using System;

namespace gitWeb.Core.Features.Configuration
{
    public class GitConfigurationProvider
    {
        private readonly RepositoryPathValdiator _repositoryPathValdiator;
        private readonly IConfigurationRepository _configRepository;

        public GitConfigurationProvider(RepositoryPathValdiator repositoryPathValdiator, IConfigurationRepository configRepository)
        {
            _repositoryPathValdiator = repositoryPathValdiator;
            _configRepository = configRepository;
        }

        public void SaveRepositoryPath(string path)
        {
            if (_repositoryPathValdiator.IsInvalid(path)) throw new ArgumentException(nameof(path));

            _configRepository.SavePath(path);
        }

        public string LoadRepositoryPath()
        {
            return _configRepository.LoadPath();
        }
    }
}