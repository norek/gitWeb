using System;
using System.Linq;
using LibGit2Sharp;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace gitWeb.Tests
{
    public class CommitCreator
    {
        private string ShaCreator()
        {
            string sha = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                sha += Guid.NewGuid().ToString();
            }

            return sha.Replace("-", "").Substring(0, 40);
        }

        public Commit CreateNewCommit(params string[] parents)
        {
            var newSha = ShaCreator();
            var commit = new Commit
            {
                Sha = newSha,
                ParentsIds = parents.Select(p => new ObjectId(p)).ToList(),
                Parents = parents.ToList()
            };

            return commit;
        }
    }
}