using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.ClientIpCheck;
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
    //[ServiceFilter(typeof(ClientIpCheckActionFilter))]
    public class NoorProfileCaseHistoryController : ControllerBase
    {
        private readonly ICaseHistoryRepository _caseHistoryRepository;
        public NoorProfileCaseHistoryController(ICaseHistoryRepository caseHistoryRepository)
        {
            _caseHistoryRepository = caseHistoryRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var response = await _caseHistoryRepository.GetReportForCaseAsync(id);
            if (response.Success)
            {
                if (response?.Data?.CaseHistoriesNoAnswer!=null)
                {
                    response.Data.CaseHistoriesNoAnswer = null;
                }
            }
            return Ok(response);
        }
    }
}
