using LibGit2Sharp;
using System.Collections.Generic;

namespace gitWeb.Core.Features.Commit
{
    public interface ICommitProvider
    {
        CommitDetail GetCommitDetails(string sha);
        IEnumerable<Commit> GetAllFromHead();
        IEnumerable<Commit> GetAllFromTip(string tipSha);
        void Commit(string message, Signature author);
    }
}