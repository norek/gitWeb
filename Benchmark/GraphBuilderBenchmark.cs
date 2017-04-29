using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using gitWeb.Core.Features.Clone;
using gitWeb.Core.Features.Commit;
using gitWeb.Core.GraphBuilder;
using LibGit2Sharp;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace Benchmark
{
    public class GraphBuilderBenchmark:IDisposable
    {
        private GraphBuilder _builder;
        private Repository _repository;
        private Commit[] _commits;

        public GraphBuilderBenchmark()
        {
            _builder = new GraphBuilder();
            _repository = new Repository("C:\\inPOS");
            CommitProvider provider = new CommitProvider(_repository);
            _commits = provider.GetAllFromHead().ToArray();
        }

        [Benchmark]
        public void Run()
        {
            _builder.Build(_commits);
        }

        public void Dispose()
        {
            if (_repository != null) _repository.Dispose();
        }
    }
}
