using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using gitWeb.Core.Features.Commit;
using gitWeb.Core.GraphBuilder;
using LibGit2Sharp;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace Benchmark
{
    public class LinkBuilderBenchmark
    {
        private LinkBuilder _linkBuilder;
        private List<Commit> _commits;

        public LinkBuilderBenchmark()
        {
            _linkBuilder = new LinkBuilder();

            using (var _repository = new Repository("C:\\inPOS"))
            {
                var provider = new CommitProvider(_repository);
                _commits = provider.GetAllFromHead().ToList();
            }
        }

        [Benchmark]
        public void Build()
        {
            _linkBuilder.Build(_commits);
        }
    }
}
