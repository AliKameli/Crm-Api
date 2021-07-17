using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports;
using CRCIS.Web.INoor.CRM.Utility.Queries;
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
    public class ReportPersonController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportPersonController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
          [FromQuery] int pageIndex,
          [FromQuery] string sortField,
          [FromQuery] string userId,
          [FromQuery] SortOrder? sortOrder)
        {
            
            var query = new PersonReportQuery(pageIndex, pageSize,userId ,sortField, sortOrder);
            var response = await _reportRepository.GetPersonReport(query);

            return Ok(response);
        }
    }
}
