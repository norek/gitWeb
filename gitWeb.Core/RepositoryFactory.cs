using gitWeb.Core.Features.Configuration;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core
{
    public class RepositoryFactory
    {
        IConfigurationRepository _configRepo;

        public RepositoryFactory(IConfigurationRepository configRepo)
        {
            _configRepo = configRepo;
        }

        public IRepository GetRepository()
        {
            var repositoryPath = _configRepo.LoadPath();
            return new Repository(repositoryPath);
        }
    }
}
