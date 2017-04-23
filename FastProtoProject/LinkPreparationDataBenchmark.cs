using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using gitWeb.Core.Features.Commit;
using LibGit2Sharp;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace FastProtoProject
{
    public class LinkPreparationDataBenchmark
    {
        private IEnumerable<Commit> _data;

        public LinkPreparationDataBenchmark()
        {
            var _repository = new Repository(@"C:\Projects\roslyn");
            var _provider = new CommitProvider(_repository);
            _data = _provider.GetAllFromHead();
        }

        [Benchmark]
        public void PrepareDictionary()
        {
            var _DictionaryListOfCommits = _data.ToDictionary(k => k.Sha, v => v);
        }

        [Benchmark]
        public void PrepareArray()
        {
            var _ArrayOfCommits = _data.ToArray();

        }

        [Benchmark]
        public void PrepareList()
        {
            var _ListOfCommits = _data.ToList();
        }
    }
}
