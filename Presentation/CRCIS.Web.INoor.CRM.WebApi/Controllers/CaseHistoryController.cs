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
    public class CaseHistoryController : ControllerBase
    {
        private readonly ICaseHistoryRepository _caseHistoryRepository;
        public CaseHistoryController(ICaseHistoryRepository caseHistoryRepository)
        {
            _caseHistoryRepository = caseHistoryRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var response = await _caseHistoryRepository.GetReportForCaseAsync(id);
            return Ok(response);
        }
    }
}
