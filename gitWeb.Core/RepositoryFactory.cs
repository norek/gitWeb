using gitWeb.Core.Features.Configuration;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core
{
    public class RepositoryFactory : IDisposable
    {
        readonly IConfigurationRepository _configRepo;
        readonly Dictionary<string, Repository> repositoryContainer = new Dictionary<string, Repository>();
        private Repository vrcRepository;

        public RepositoryFactory(IConfigurationRepository configRepo)
        {
            _configRepo = configRepo;
        }

        public IRepository GetRepository()
        {
            var repositoryPath = _configRepo.LoadPath();
            vrcRepository = new Repository(repositoryPath);
            return vrcRepository;
        }

        public void Dispose()
        {

        }
    }
}
