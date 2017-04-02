using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitWeb.Core.Features.Branch
{
    public class BranchProvider: IBranchProvider
    {

        private readonly IRepository _repository;

        public BranchProvider(IRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return _repository.Branches
                              .Select(b => new Branch() { IsRemote = b.IsRemote,Name = b.FriendlyName})
                              .ToList();
        }
    }

    public interface IBranchProvider
    {
        IEnumerable<Branch> GetAllBranches();
    }

    public class Branch
    {
        public string Name { get; set; }

        public bool IsRemote { get; set; }
    }
}
