using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.AdminCardboard.Queries;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportAdminCardBoardController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportAdminCardBoardController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
             [FromQuery] int pageIndex,
             [FromQuery] string sortField,
             [FromQuery] SortOrder? sortOrder,
             [FromQuery] string adminIds = null,
             [FromQuery] string sourceTypeIds = null,
             [FromQuery] string productIds = null,
             [FromQuery] string title = null,
             [FromQuery] string global = null,
             [FromQuery] string range = null)
        {
            var query = new AdminCardboardReportQuery(pageIndex, pageSize,
              sortField, sortOrder,
              sourceTypeIds, productIds, adminIds,
              title, global, range
              );

            var resposne = await _reportRepository.GetAdminCardboardReportAsync(query);

            return Ok(resposne);
        }
    }
}
