using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using gitWeb.Core.Features.Commit;
using gitWeb.Core.GraphBuilder;
using LibGit2Sharp;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace FastProtoProject
{
    //[MemoryDiagnoser]
    //[InliningDiagnoser]
    public class LinkCollectionBenchmark : IDisposable
    {
        private ICommitProvider _provider;
        private List<Commit> _ListOfCommits;
        private IEnumerable<Commit> _EnumerableOfCommits;
        private Repository _repository;
        private Dictionary<string, Commit> _DictionaryListOfCommits;
        private Commit[] _ArrayOfCommits;

        public LinkCollectionBenchmark()
        {
            _repository = new Repository(@"C:\Projects\roslyn\roslyn");
            _provider = new CommitProvider(_repository);
        }

        [Setup]
        public void SetUpCommits()
        {
            _ListOfCommits = _provider.GetAllFromHead().ToList();
            _DictionaryListOfCommits = _provider.GetAllFromHead().ToDictionary(k => k.Sha, v => v);
            _ArrayOfCommits = _provider.GetAllFromHead().OrderByDescending(d => d.Date).ToArray();
        }


        [Benchmark(Baseline = true)]
        public void Run_LinqApproach()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildNoOptimalizedLinq(_ListOfCommits);
        }

        [Benchmark]
        public void Run_DictionaryApproach()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.Build(_DictionaryListOfCommits);
        }

        [Benchmark]
        public void Run_ArrayApproach_WithLinq()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.Build_On_Array_WithLinq(_ArrayOfCommits);
        }

        [Benchmark]
        public void Run_ArrayApproach_WithForLoopSearching()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildOnArray_ForLoop(_ArrayOfCommits);
        }

        [Benchmark]
        public void Run_ArrayApproach_WithForLoopSearching2()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildOnArray_ForLoop2(_ArrayOfCommits);
        }

        [Benchmark]
        public void Run_ArrayApproach_WithArrayFindAllMethod()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildNoOptimalizedLinqOnArrayFind(_ArrayOfCommits);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}

