﻿using CRCIS.Web.INoor.CRM.Contract.Service;
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
    public class NoorAppCommentByCaseIdController : ControllerBase
    {
        private readonly INoorlockCommentService _noorlockCommentService;
        public NoorAppCommentByCaseIdController(INoorlockCommentService noorlockCommentService)
        {
            _noorlockCommentService = noorlockCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long caseId)
        {
            var respose = await _noorlockCommentService.GetNoorAppGetByCaseId(caseId);
            return Ok(respose);
        }
    }
}
