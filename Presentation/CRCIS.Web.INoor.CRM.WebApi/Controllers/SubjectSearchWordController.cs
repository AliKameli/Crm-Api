using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
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
    public class SubjectSearchWordController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectSearchWordController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string searchWord)
        {

            return Ok();
        }
    }
}
