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
    public class SubjectDropDownListController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectDropDownListController(ISubjectRepository subjectRepository )
        {
            _subjectRepository = subjectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _subjectRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
