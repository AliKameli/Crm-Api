using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportOperatorController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportOperatorController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [JwtAuthorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
             [FromQuery] int pageIndex,
             [FromQuery] string sortField,
             [FromQuery] SortOrder? sortOrder,
             [FromQuery] string adminIds= null,
             [FromQuery] string sourceTypeIds = null,
             [FromQuery] string operationTypeIds = null,
             [FromQuery] string productIds = null,
             [FromQuery] string title = null,
             [FromQuery] string global = null,
             [FromQuery] string range = null,
             [FromQuery] string rangeOperationDate = null)
        {
            var query = new OperatorReportQuery(pageIndex, pageSize,
              sortField, sortOrder,
              operationTypeIds,sourceTypeIds, productIds,adminIds,
              title, global, range,rangeOperationDate
              );

            var resposne = await _reportRepository.GetOperatorReportAsync(query);

            return Ok(resposne);
        }
    }
}
