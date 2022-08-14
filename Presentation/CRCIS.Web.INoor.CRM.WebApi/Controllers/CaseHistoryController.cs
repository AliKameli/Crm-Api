using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Queries;
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
            var query = new CasePendingHistoryReportByCaseIdQuery(id, "1,2,3,4,5");
            var response = await _caseHistoryRepository.CasePendingHistoryReportByCaseIdAsync(query);
            return Ok(response);
        }
    }
}