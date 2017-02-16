using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace gitWeb.IntegrationTests
{
    public class GitConfigurationProviderShould
    {
        string _validRepoPath = @"C:\Projects\Own\DSP\gitWeb";

        [Fact]
        public void WhenSectionIsNotConfigured_CreateSection_AndSavePath()
        {
            GitConfigurationProvider provider = new GitConfigurationProvider(new RepositoryPathValdiator());
            Assert.True(provider.SaveRepositoryPath(_validRepoPath));
        }

        [Fact]
        public void OverrideExistingPath()
        {
            GitConfigurationProvider provider = new GitConfigurationProvider(new RepositoryPathValdiator());
            Assert.True(provider.SaveRepositoryPath(_validRepoPath));
        }

        [Fact]
        public void WhenLoadIsCalled_ReturnSavedPath()
        {
            GitConfigurationProvider provider = new GitConfigurationProvider(new RepositoryPathValdiator());
            provider.SaveRepositoryPath(_validRepoPath);

            var path = provider.LoadRepositoryPath();

            Assert.Equal(_validRepoPath, path);
        }
    }
}
