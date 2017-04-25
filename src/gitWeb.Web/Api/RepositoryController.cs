using gitWeb.Core.Features.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gitWeb.Core.Features.Repository;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/repository")]
    public class RepositoryController : ApiController
    {
        private readonly IStagingAreaProvider _stagingAreaProvider;

        public RepositoryController(IStagingAreaProvider stagingAreaProvider)
        {
            _stagingAreaProvider = stagingAreaProvider;
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
    }
}
