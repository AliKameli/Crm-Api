using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectChildrenController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectChildrenController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? parentId)
        {
            var query = new SubjectChildrenDataTableQuery(parentId);
            var response = await _subjectRepository.GetChildrenAsync(query);
            return Ok(response);
        }
    }
}