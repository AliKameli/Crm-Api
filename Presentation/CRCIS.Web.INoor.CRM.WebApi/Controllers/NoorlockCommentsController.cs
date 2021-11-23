using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoorlockCommentsController : ControllerBase
    {
        private readonly INoorlockCommentService _noorlockCommentService;
        public NoorlockCommentsController(INoorlockCommentService noorlockCommentService)
        {
            _noorlockCommentService = noorlockCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] Guid? inoorId = null,
            [FromQuery] string commentNo = null,
            [FromQuery] string typeOfComment = null,
            [FromQuery] string snId = null,
            [FromQuery] string sk = null,
            [FromQuery] string activationCode = null,
            [FromQuery] string secret = null)
        {

            var query = 
                new NoorLockReportPagingQuery(
                    inoorId, commentNo, typeOfComment, snId, sk, activationCode, secret,
                    pageIndex, pageSize);

            return Ok();
        }
    }
}
