using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibGit2Sharp;

namespace gitWeb.Core.Features.Stage
{
    class StagingAreaProvider
    {
        private readonly IRepository _repository;

        public StagingAreaProvider(IRepository repository)
        {
            _repository = repository;
        }

        public void Stage(IEnumerable<string> paths)
        {
            if (paths == null) throw new ArgumentException(nameof(paths));

            Commands.Stage(_repository, paths);
        }

        public void UnStage(IEnumerable<string> paths)
        {
            if (paths == null) throw new ArgumentException(nameof(paths));

            Commands.Unstage(_repository, paths);
        }
    }
}
