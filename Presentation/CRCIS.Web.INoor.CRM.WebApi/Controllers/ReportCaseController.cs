using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using Microsoft.AspNetCore.Authorization;
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
    public class ReportCaseController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportCaseController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
          [FromQuery] int pageIndex,
          [FromQuery] string sortField,
          [FromQuery] string userId,
          [FromQuery] SortOrder? sortOrder,
          [FromQuery] string sourceTypeIds = null,
          [FromQuery] string productIds = null,
          [FromQuery] string title = null,
          [FromQuery] string global = null,
          [FromQuery] string range = null)
        {
            var query = new Domain.Reports.Case.Queries.CaseReportQuery(pageIndex, pageSize,
                sortField, sortOrder,
                sourceTypeIds, productIds,
                title, global, range
                );
            var resposne = await _reportRepository.GetCaseReportAsync(query);
            return Ok(resposne);
        }
    }
}
