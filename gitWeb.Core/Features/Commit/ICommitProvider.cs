using System.Collections.Generic;

namespace gitWeb.Core.Features.Commit
{
    public interface ICommitProvider
    {
        CommitDetail GetCommitDetails(string sha);
        IEnumerable<Commit> GetAllFromHead();
    }
}