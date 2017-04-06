using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.Features.Branch
{
    public class BranchProvider : IBranchProvider
    {

        private readonly IRepository _repository;

        public BranchProvider(IRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return _repository.Branches
                              .Select(b => new Branch() { IsRemote = b.IsRemote, Name = b.FriendlyName })
                              .ToList();
        }

        public void Create(string branchName)
        {
            if (string.IsNullOrEmpty(branchName)) throw new ArgumentNullException(nameof(branchName));
            _repository.CreateBranch(branchName);
        }
    }

    public interface IBranchProvider
    {
        IEnumerable<Branch> GetAllBranches();
        void Create(string branchName);
    }

    public class Branch
    {
        public string Name { get; set; }

        public bool IsRemote { get; set; }
    }
}
