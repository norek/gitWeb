using gitWeb.Core.Features.Stage;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gitWeb.Web.Api
{
    [Route("api/filechange")]
    public class FileChangesController : ApiController
    {
        private readonly IFileChangeProvider _changeProvider;

        public FileChangesController(IFileChangeProvider changeProvider)
        {
            _changeProvider = changeProvider;
        }

        [HttpGet]
        public IHttpActionResult Get(string path)
        {
            return Ok(_changeProvider.GetFileDiff(path));
        }
    }
}
