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
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex, [FromQuery] int? parentId,
            [FromQuery] string title = null, [FromQuery] string code = null)
        {
            var query = new SubjectChildrenDataTableQuery(pageIndex, pageSize, parentId, title, code);
            var response = await _subjectRepository.GetChildrenAsync(query);
            return Ok(response);
        }
    }
}