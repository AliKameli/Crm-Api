using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.Subject.Queries;
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
    public class ReportSubjectController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportSubjectController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
          [FromQuery] int pageIndex,
          [FromQuery] string sortField,
          [FromQuery] SortOrder? sortOrder,
          [FromQuery] string sourceTypeIds = null,
          [FromQuery] string subjectIds = null,
          [FromQuery] string productIds = null,
          [FromQuery] string global = null,
          [FromQuery] string range = null)
        {
            var query = new SubjectReportQuery(pageIndex, pageSize,
                sortField, sortOrder,
                productIds, subjectIds, sourceTypeIds,
                global, range);
            var response = await _reportRepository.GetSubjectReportAsync(query);

            return Ok(response);
        }
    }
}
