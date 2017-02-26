using System;
using LibGit2Sharp;

namespace gitWeb.Core.Features.Commit
{
    public class CommitProvider
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
    }
}