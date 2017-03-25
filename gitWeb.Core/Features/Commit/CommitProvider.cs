using System;
using LibGit2Sharp;
using System.Linq;

namespace gitWeb.Core.Features.Commit
{
    public class CommitProvider:ICommitProvider
    {
        private readonly IRepository _repository;

        public CommitProvider(IRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }

        public void Commit(string message, Signature author)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException();
            if (author == null) throw new ArgumentNullException();

            var committer = author;

            _repository.Commit(message, author, committer);
        }

        public CommitDetail GetCommitDetails(string sha)
        {
            if (string.IsNullOrEmpty(sha)) throw new ArgumentNullException();

            var commit = _repository.Lookup<LibGit2Sharp.Commit>(sha);

            if (commit == null) throw new CommitNotFoundException(sha);

            var commitParent = commit.Parents.Last();

            TreeChanges treeChanges = _repository.Diff.Compare<TreeChanges>(commitParent.Tree, commit.Tree);

            CommitDetail commitDetail = new CommitDetail(sha, commit.Message, commit.Author.Name, commit.Author.When.Date);
            commitDetail.Files.AddRange(treeChanges.Added.Select(s => new CommitFile((int)s.Status, s.Path)));
            commitDetail.Files.AddRange(treeChanges.Modified.Select(s => new CommitFile((int)s.Status, s.Path)));
            commitDetail.Files.AddRange(treeChanges.Deleted.Select(s => new CommitFile((int)s.Status, s.Path)));

            return commitDetail;
        }
    }
}