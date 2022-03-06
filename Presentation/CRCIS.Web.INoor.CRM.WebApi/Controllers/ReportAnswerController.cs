using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportAnswerController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportAnswerController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
         [FromQuery] int pageIndex,
         [FromQuery] string sortField,
         [FromQuery] SortOrder? sortOrder,
         [FromQuery] string sourceTypeIds = null,
         [FromQuery] string productIds = null,
         [FromQuery] string title = null,
         [FromQuery] string global = null,
         [FromQuery] string range = null)
        {
          
            var query = new Domain.Reports.Answer.Queries.AnswerReportQuery(pageIndex, pageSize,
                sortField, sortOrder,
                sourceTypeIds, productIds,
                title, global, range
                );


            var resposne = await _reportRepository.GetAnsweringReportAsync(query);

            return Ok(resposne);

        }
    }
}
