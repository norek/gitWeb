using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core;
using gitWeb.Core.Features.Status;
using LibGit2Sharp;
using NSubstitute;
using Xunit;

namespace gitWeb.Tests
{
    public class StatusProviderShould
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowAnException_WhenSuppliedPath_IsInvalid(string path)
        {
            StatusProvider statusProvider = new StatusProvider(null);
            Assert.Throws<ArgumentException>(() => statusProvider.Get(path));
        }

        [Fact]
        public void CallLibgit_RetrieveStatus()
        {
            var repository = NSubstitute.Substitute.For<IRepository>();
            StatusProvider statusProvider = new StatusProvider(repository);
            var filePath = "somePath";

            statusProvider.Get(filePath);

            repository.Received().RetrieveStatus(filePath);
        }

        public void test()
        {
            FileChangeProvider
        }
    }
}
