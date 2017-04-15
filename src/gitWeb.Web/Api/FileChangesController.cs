using gitWeb.Core.Features.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/filechange")]
    public class FileChangeController : ApiController
    {
        private readonly IFileChangeProvider _changeProvider;

        public FileChangeController(IFileChangeProvider changeProvider)
        {
            _changeProvider = changeProvider;
        }

        [HttpGet]
        public IHttpActionResult Get(string filePath)
        {
            return Ok(_changeProvider.GetFileDiff(filePath));
        }

        [HttpPost]
        [Route("discard/all")]
        public IHttpActionResult DiscardAll()
        {
            _changeProvider.DiscardAllChanges();
            return Ok();
        }

        [HttpPost]
        [Route("discard")]
        public IHttpActionResult DiscardFileChanges(string filePath)
        {
            _changeProvider.DiscardFileChanges(filePath);
            return Ok();
        }
    }
}
