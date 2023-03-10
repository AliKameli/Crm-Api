using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
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
    public class ReportDashboardProductHistoryChartController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportDashboardProductHistoryChartController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _reportRepository.GetProductHistoryReportAsync();
            return Ok(response);
        }
    }

}
