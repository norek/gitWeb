using gitWeb.Core.Features.Branch;
using gitWeb.Core.Features.Commit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gitWeb.Core.GraphBuilder;

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

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var commits = _commitProvider.GetAllFromHead().Take(1000);

            Core.GraphBuilder.GraphBuilder builder = new GraphBuilder();
            var commitArray = commits.ToArray();
            var convertedCommits = builder.Build(commitArray);

            LinkBuilder linkBuilder = new LinkBuilder();
            var links = linkBuilder.Build(convertedCommits.ToList());

            return Ok(new { commits = convertedCommits, links });
        }

        [HttpGet]
        [Route("{sha}")]
        public IHttpActionResult Get(string sha)
        {
            var commitDetails = _commitProvider.GetCommitDetails(sha);
            return Ok(commitDetails);
        }

        [HttpGet]
        [Route("{tiSha}/tree")]
        public IHttpActionResult GetFromBranchTip(string tipSha)
        {
            var commitDetails = _commitProvider.GetAllFromTip(tipSha);
            return Ok(commitDetails);
        }

        [HttpPost]
        public IHttpActionResult Commit([FromBody] CommitParams commitParams)
        {
            _commitProvider.Commit(commitParams.Message, null);
            return Ok();
        }

    }
}
