using gitWeb.Core.Features.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/repository")]
    public class RepositoryController : ApiController
    {
        private IStagingAreaProvider _stagingAreaProvider;

        public RepositoryController(IStagingAreaProvider stagingAreaProvider)
        {
            _stagingAreaProvider = stagingAreaProvider;
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
