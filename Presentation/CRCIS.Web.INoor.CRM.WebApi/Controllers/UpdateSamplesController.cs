using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
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
    public class UpdateSamplesController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        public UpdateSamplesController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put()
        {
            var res = await _subjectRepository.UpdateSampleAsync();
            return Ok(res);
        }
    }
}
