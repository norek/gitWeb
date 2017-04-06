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

        public Branch Checkout(string branchName)
        {
            if (string.IsNullOrEmpty(branchName)) throw new ArgumentNullException(nameof(branchName));


            var branch = _repository.Branches[branchName];

            if (branch == null)
            {
                return null;
            }

            LibGit2Sharp.Branch currentBranch = Commands.Checkout(_repository, branch);
            return new Branch(currentBranch.FriendlyName, currentBranch.IsRemote); 
        }

    }

    public interface IBranchProvider
    {
        IEnumerable<Branch> GetAllBranches();
        Branch Checkout(string branchName);
        void Create(string branchName);
    }

    public class Branch
    {
        public Branch()
        {

        }

        public Branch(string name,bool isRemote)
        {
            this.Name = name;
            this.IsRemote = isRemote;
        }

        public string Name { get; set; }

        public bool IsRemote { get; set; }
    }
}
