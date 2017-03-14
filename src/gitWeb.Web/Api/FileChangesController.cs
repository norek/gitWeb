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
    public class FileChangesController : ApiController
    {
        string path = @"C:\Projects\Own\DSP\gitWeb";

        // GET: api/FileChanges
        public IHttpActionResult Get()
        {
            FileChangeProvider sd = new FileChangeProvider(new Repository(path));
            var result = sd.GetFileDiff(@"C:\Projects\Own\DSP\gitWeb\src\gitWeb.Web\ViewModels\Home\components\unstagedFiles.vue");
            return Ok(result);
        }

        // GET: api/FileChanges/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FileChanges
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FileChanges/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FileChanges/5
        public void Delete(int id)
        {
        }
    }
}
