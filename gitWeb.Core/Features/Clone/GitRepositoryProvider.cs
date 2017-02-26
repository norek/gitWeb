using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core.Features.Configuration;
using LibGit2Sharp;

namespace gitWeb.Core.Features.Clone
{
    public class GitRepositoryProvider
    {
        private readonly GitConfigurationProvider _repositorySettingsProvider;

        public GitRepositoryProvider(GitConfigurationProvider repositorySettingsProvider)
        {
            _repositorySettingsProvider = repositorySettingsProvider;
        }

        public void Clone(string url, string path)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(path));

            string repoPath = Repository.Clone(url, path);

            _repositorySettingsProvider.SaveRepositoryPath(repoPath);
        }

        public void Init()
        {

        }
    }
}
