using gitWeb.Core.Features.Branch;
using gitWeb.Core.Features.Commit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gitWeb.Web.Api
{
    [RoutePrefix("api/branch")]
    public class BranchController : ApiController
    {
        private readonly IBranchProvider _branchProvider;

        public BranchController(IBranchProvider branchProvider)
        {
            _branchProvider = branchProvider;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_branchProvider.GetAllBranches());
        }
    }
}
