using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core.Features.Configuration;

namespace gitWeb.Core.Features.Clone
{
    public class GitRepositoryProvider
    {
        public void Clone(string url, string path)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            LibGit2Sharp.Repository.Clone(url, path);
        }
    }
}
