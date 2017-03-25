using gitWeb.Core.Features.Commit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/commit")]
    public class CommitController : ApiController
    {
        private readonly ICommitProvider _commitProvider;

        public CommitController()
        {

        }

        public CommitController(ICommitProvider commitProvider)
        {
            _commitProvider = commitProvider;
        }

        // GET: api/Commit
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("{sha}")]
        public IHttpActionResult Get(string sha)
        {
            var commitDetails = _commitProvider.GetCommitDetails(sha);
            return Ok(commitDetails);
        }

    }


}
