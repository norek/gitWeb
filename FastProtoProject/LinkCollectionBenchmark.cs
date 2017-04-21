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
            _repository = new Repository(@"C:\Projects\Taksonomia");
            _provider = new CommitProvider(_repository);
        }

        [Setup]
        public void SetUpCommits()
        {
            _ListOfCommits = _provider.GetAllFromHead().OrderBy(a => Guid.NewGuid()).ToList();
            _DictionaryListOfCommits = _ListOfCommits.OrderBy(a => Guid.NewGuid()).ToDictionary(k => k.Sha, v => v);
            _ArrayOfCommits = _provider.GetAllFromHead().OrderBy(a => Guid.NewGuid()).ToArray();

            Console.WriteLine("Number of elements: " + _ListOfCommits.Count);
        }

        [Benchmark(Baseline = true)]
        public void RunClassicLinqApproach()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildNoOptimalizedLinq(_ListOfCommits);
        }

        [Benchmark]
        public void RunDictionaryBenchmark_sexi()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildSexiLinqChain(_ListOfCommits);
        }

        [Benchmark]
        public void RunDictionaryBenchmark()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.Build(_DictionaryListOfCommits);
        }

        [Benchmark]
        public void RunDictionaryArrayLinq()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.Build_On_Array_WithLinq(_ArrayOfCommits);
        }

        [Benchmark]
        public void RunDictionaryArrayFor()
        {
            LinkBuilder builder = new LinkBuilder();
            builder.BuildOnArray_ForLoop(_ArrayOfCommits);
        }

        [Benchmark]
        public void RunDictionaryArrayFind()
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
