using System;

namespace gitWeb.Core.Features.Commit
{
    public class CommitNotFoundException : Exception
    {
        public CommitNotFoundException(string sha) : base("Commit not found: " + sha)
        {
            Data.Add("sha", sha);
        }
    }
}