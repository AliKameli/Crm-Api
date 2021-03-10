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
    public class CaseStatusDropDownListController : ControllerBase
    {
        private readonly ICaseStatusRepository _caseStatusRepository;

        public CaseStatusDropDownListController(ICaseStatusRepository caseStatusRepository)
        {
            _caseStatusRepository = caseStatusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _caseStatusRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
