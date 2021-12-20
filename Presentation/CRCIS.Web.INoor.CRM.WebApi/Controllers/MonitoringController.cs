using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILogger _logger;

        public MonitoringController(IReportRepository reportRepository,ILoggerFactory loggerFactory)
        {
            _reportRepository = reportRepository;
            _logger = loggerFactory.CreateLogger<MonitoringController>();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _reportRepository.GetCaseHistoryReportAsync();
            _logger.LogCritical(System.Text.Json.JsonSerializer.Serialize(response));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            _logger.LogCritical(System.Text.Json.JsonSerializer.Serialize(DateTime.Now));
            return Ok();
        }
    }
}
