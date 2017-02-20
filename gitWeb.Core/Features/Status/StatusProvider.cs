using System;
using LibGit2Sharp;

namespace gitWeb.Core.Features.Status
{
    public class StatusProvider
    {
        private readonly IRepository _repo;

        public StatusProvider(IRepository repo)
        {
            _repo = repo;
        }

        public FileStatus Get(string filePath)
        {
            if(string.IsNullOrEmpty(filePath)) throw new ArgumentException(nameof(filePath));

            return _repo.RetrieveStatus(filePath);
        }
    }
}
