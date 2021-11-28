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
    public class NoorAppCommentsController : ControllerBase
    {
        private readonly INoorlockCommentService _noorlockCommentService;
        public NoorAppCommentsController(INoorlockCommentService noorlockCommentService)
        {
            _noorlockCommentService = noorlockCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] Guid inoorId ,
            [FromQuery] string productSecret )
        {

            var response =await _noorlockCommentService.GetNoorAppReportPaging(pageSize, pageIndex,
                inoorId, productSecret);

            return Ok(response);
        }
    }
}
