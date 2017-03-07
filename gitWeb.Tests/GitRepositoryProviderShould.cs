using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core.Features.Clone;
using gitWeb.Core.Features.Configuration;
using Xunit;

namespace gitWeb.Tests
{

    //TODO: Dopisać testy do tych statycznych metod
    public class GitRepositoryProviderShould
    {
        [Fact]
        public void ThrowAnArgumentExceptionWhen_CtorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new GitRepositoryProvider(null));
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenCloning_ThrowAnArgumentExceptionWhen_PathIsEmpty(string path)
        {
            var configMock = NSubstitute.Substitute.For<GitConfigurationProvider>(null,null);
            GitRepositoryProvider provider = new GitRepositoryProvider(configMock);
            Assert.Throws<ArgumentNullException>(() => provider.Clone(path, "somePath"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenCloning_ThrowAnArgumentExceptionWhen_UrlIsEmpty(string url)
        {
            var configMock = NSubstitute.Substitute.For<GitConfigurationProvider>(null, null);
            GitRepositoryProvider provider = new GitRepositoryProvider(configMock);
            Assert.Throws<ArgumentNullException>(() => provider.Clone("somePath", url));
        }
    }
}
