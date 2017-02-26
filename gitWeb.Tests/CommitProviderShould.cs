using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core.Features.Commit;
using LibGit2Sharp;
using NSubstitute;
using Xunit;

namespace gitWeb.Tests
{
    public class CommitProviderShould : TestFixtureBase
    {
        [Fact]
        public void ThrowAnExceptionWhen_CtorParamsAreNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CommitProvider(null));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowAnExceptionWhen_MessageIsNullOrEmpty(string message)
        {
            var repository = NSubstitute.Substitute.For<IRepository>();
            var provider = new CommitProvider(repository);

            Assert.Throws<ArgumentNullException>(() => provider.Commit(message, Any<Signature>()));
        }


        [Fact]
        public void ThrowAnExceptionWhen_SignatureIsNull()
        {
            var repository = NSubstitute.Substitute.For<IRepository>();
            var provider = new CommitProvider(repository);

            Assert.Throws<ArgumentNullException>(() => provider.Commit("message", null));
        }

        [Fact]
        public void CallCommitMethod()
        {
            var repository = NSubstitute.Substitute.For<IRepository>();
            var provider = new CommitProvider(repository);
            var signature = new Signature("sd", "sd", DateTimeOffset.Now);

            provider.Commit("message", signature);
            repository.Received().Commit("message", signature, signature);
        }
    }
}
