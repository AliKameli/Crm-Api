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
    public class NoorlockCommentByRowNumberController : ControllerBase
    {
        private readonly INoorlockCommentService _noorlockCommentService;
        public NoorlockCommentByRowNumberController(INoorlockCommentService noorlockCommentService)
        {
            _noorlockCommentService = noorlockCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] long rowNumber,
            //[FromQuery] Guid? inoorId = null,
            [FromQuery] string typeOfComment = null,
            [FromQuery] long? snId = null,
            [FromQuery] string sk = null,
            [FromQuery] string activationCode = null,
            [FromQuery] string productSecret = null)
        {
            var response =await _noorlockCommentService.GetNoorlockGetByRowNumber(rowNumber,
                null, typeOfComment, snId, sk, activationCode, productSecret);

            return Ok(response);
        }
    }
}
