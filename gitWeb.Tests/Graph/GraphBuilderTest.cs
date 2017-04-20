using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core.Features.Commit;
using gitWeb.Core.GraphBuilder;
using LibGit2Sharp;
using Xunit;
using Commit = gitWeb.Core.Features.Commit.Commit;

namespace gitWeb.Tests.Graph
{
    public class GraphBuilderTest:IDisposable
    {
        private Repository _repository;
        private Commit[] commits;
        private GraphBuilder builder;

        public GraphBuilderTest()
        {
            _repository = new Repository(@"C:\Projects\Taksonomia");
            CommitProvider provider = new CommitProvider(_repository);
            commits = provider.GetAllFromHead().ToArray();
            builder = new GraphBuilder();

        }

        [Fact]
        public void CreateGraphStrucure()
        {
            builder.Build(commits);

            Assert.Equal(2, commits.Single(d => d.Sha == "61242da452419f089a4a19fb1bbef62c22aa49db").HIndex);
            Assert.Equal(2, commits.Single(d => d.Sha == "6cfeee2f3e0a9aa27116897136490cd8a07736af").HIndex);
            Assert.Equal(3, commits.Single(d => d.Sha == "7c3c3f2de5d5b16858632e53aa8ef1fce4c05ccb").HIndex);
            Assert.Equal(4, commits.Single(d => d.Sha == "0bd7bc4e5a0601ed709552966f258fa52fcbebf5").HIndex);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
