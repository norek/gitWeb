using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gitWeb.Core;
using gitWeb.Core.Features.Configuration;
using Xunit;

namespace gitWeb.Tests
{
    public class RepositoryPathValdiatorShould
    {
        [Fact]
        public void Return_InvalidPathResult_WhenPassedPath_IsInvalid()
        {
            string invalidPath = @"C\yoyo\xx\ww";

            RepositoryPathValdiator validator = new RepositoryPathValdiator();
            var result = validator.IsInvalid(invalidPath);
            Assert.True(result);
        }

        [Fact]
        public void Return_InvalidGitRepository_WhenPassedPath_IsNotGitRepository()
        {
            string noGitPath = @"C:\Windows\";

            RepositoryPathValdiator validator = new RepositoryPathValdiator();
            var result = validator.IsInvalid(noGitPath);

            Assert.True(result);
        }

        [Fact]
        public void Return_OkStatus_WhenPath_isValid()
        {
            string valdiPath = @"C:\Projects\Own\DSP\gitWeb";

            RepositoryPathValdiator validator = new RepositoryPathValdiator();
            var result = validator.IsInvalid(valdiPath);

            Assert.False(result);
        }
    }
}
