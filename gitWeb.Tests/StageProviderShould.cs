using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core.Features.Stage;
using LibGit2Sharp;
using Xunit;
using Assert = Xunit.Assert;

namespace gitWeb.Tests
{
    public class StageProviderShould
    {
        IRepository _repo;

        public StageProviderShould()
        {
            _repo = NSubstitute.Substitute.For<IRepository>();
        }

        [Fact]
        public void ThrowAnException_WhenCtorParamIsNull()
        {
            Assert.Throws<ArgumentException>(() => new StagingAreaProvider(null));
        }

        [Fact]
        public void ThrowsAnException_WhenParamIsNull_CallStageMethod()
        {
            IRepository repo = NSubstitute.Substitute.For<IRepository>();
            var provider = new StagingAreaProvider(repo);

            Assert.Throws<ArgumentException>(() => provider.Stage(null));
        }

        [Fact]
        public void ThrowsAnException_WhenParamIsNull_CallUnStageMethod()
        {
            IRepository repo = NSubstitute.Substitute.For<IRepository>();
            var provider = new StagingAreaProvider(repo);

            Assert.Throws<ArgumentException>(() => provider.UnStage(null));
        }
    }
}
