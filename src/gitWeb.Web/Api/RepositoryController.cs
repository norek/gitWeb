using gitWeb.Core.Features.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Autofac;
using gitWeb.Core.Features.Configuration;
using gitWeb.Core.Features.Repository;
using LibGit2Sharp;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/repository")]
    public class RepositoryController : ApiController
    {
        private readonly IStagingAreaProvider _stagingAreaProvider;
        private readonly IConfigurationRepository _configRepository;

        public RepositoryController(IStagingAreaProvider stagingAreaProvider, IConfigurationRepository configRepository)
        {
            _stagingAreaProvider = stagingAreaProvider;
            _configRepository = configRepository;
        }

        [HttpGet]
        [Route("directory")]
        public IHttpActionResult GetDirectory(string path)
        {
            DirectoryProvider provider = new DirectoryProvider();
            return Ok(provider.GetDirectoryList(path));
        }

        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetStatus()
        {
            return Ok(_stagingAreaProvider.GetRepositoryStatus());
        }

        [HttpPost]
        [Route("stage")]
        public IHttpActionResult StageFile(string filePath)
        {
            _stagingAreaProvider.Stage(filePath);
            return Ok();
        }

        [HttpPost]
        [Route("unstage")]
        public IHttpActionResult Unstage(string filePath)
        {
            _stagingAreaProvider.UnStage(filePath);
            return Ok();
        }

        [HttpPost]
        [Route("map")]
        public IHttpActionResult MapRepository(string path)
        {
            if (Repository.IsValid(path))
            {
                _configRepository.SavePath(path);
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("mapp")]
        public IHttpActionResult GetMappedRepository()
        {
            var mappedRepositories = _configRepository.LoadMappedRepositories();
            return Ok(mappedRepositories);
        }
    }
}
