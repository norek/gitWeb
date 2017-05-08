using gitWeb.Core.Features.Branch;
using gitWeb.Core.Features.Commit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gitWeb.Core.Features.Clone;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/remote")]
    public class RemoteController : ApiController
    {
        private readonly GitRepositoryProvider _repositoryProvider;

        public RemoteController(GitRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        [HttpPost]
        [Route("clone")]
        public IHttpActionResult Create([FromBody] CloneParams cloneParams)
        {
            _repositoryProvider.Clone(cloneParams.Url,cloneParams.Path);
            return Ok();
        }
    }

    public class CloneParams
    {
        public string Url { get; set; }
        public string Path { get; set; }
    }
}
