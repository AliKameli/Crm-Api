using CRCIS.Web.INoor.CRM.Contract.Service;
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
    public class NoorlockCommentsByTagsController : ControllerBase
    {
        private readonly INoorlockCommentService _noorlockCommentService;
        public NoorlockCommentsByTagsController(INoorlockCommentService noorlockCommentService)
        {
            _noorlockCommentService = noorlockCommentService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string typeOfComment = null,
            [FromQuery] long? snId = null,
            [FromQuery] string sk = null,
            [FromQuery] string activationCode = null,
            [FromQuery] string productSecret = null)
        {
            var response = await _noorlockCommentService.GetNoorlockPaging(pageSize, pageIndex,
                  typeOfComment, snId, sk, activationCode, productSecret);

            return Ok(response);
        }
    }
}
