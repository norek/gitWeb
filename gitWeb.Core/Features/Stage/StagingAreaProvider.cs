using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibGit2Sharp;

namespace gitWeb.Core.Features.Stage
{
    public class StagingAreaProvider : IStagingAreaProvider
    {
        private readonly IRepository _repository;

        public StagingAreaProvider(IRepository repository)
        {
            if (repository == null) throw new ArgumentException(nameof(repository));

            _repository = repository;
        }

        public void Stage(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException(nameof(path));

            Commands.Stage(_repository, path);
        }

        public void Stage(IEnumerable<string> paths)
        {
            if (paths == null) throw new ArgumentException(nameof(paths));

            Commands.Stage(_repository, paths);
        }

        public void UnStage(string path)
        {
            if (path == null) throw new ArgumentException(nameof(path));

            Commands.Unstage(_repository, path);
        }

        public void UnStage(IEnumerable<string> paths)
        {
            if (paths == null) throw new ArgumentException(nameof(paths));

            Commands.Unstage(_repository, paths);
        }

        public IEnumerable<RepositoryStatus> GetRepositoryStatus()
        {
            var retrivedStatus = _repository.RetrieveStatus();

            List<RepositoryStatus> repoStatus = new List<RepositoryStatus>();
            repoStatus.AddRange(retrivedStatus.Added.Select(d => new RepositoryStatus() { FilePath = d.FilePath, FileStatus = 1 }));
            repoStatus.AddRange(retrivedStatus.Modified.Select(d => new RepositoryStatus() { FilePath = d.FilePath, FileStatus = 2 }));
            repoStatus.AddRange(retrivedStatus.Removed.Select(d => new RepositoryStatus() { FilePath = d.FilePath, FileStatus = 3 }));
            repoStatus.AddRange(retrivedStatus.Staged.Select(d => new RepositoryStatus() { FilePath = d.FilePath, FileStatus = 4 }));

            return repoStatus;
        }
    }

    public interface IStagingAreaProvider
    {
        IEnumerable<RepositoryStatus> GetRepositoryStatus();
        void Stage(string filePath);
        void UnStage(string filePath);
    }

    public class RepositoryStatus
    {
        public string FilePath { get; set; }
        public int FileStatus { get; set; }
    }
}
