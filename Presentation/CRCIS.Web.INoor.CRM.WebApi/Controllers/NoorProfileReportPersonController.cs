using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.ClientIpCheck;
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
    //[ServiceFilter(typeof(ClientIpCheckActionFilter))]
    public class NoorProfileReportPersonController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public NoorProfileReportPersonController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
          [FromQuery] int pageIndex,
          [FromQuery] string sortField,
          [FromQuery] string userId,
          [FromQuery] SortOrder? sortOrder,
          //[FromQuery] string sourceTypeIds = null,
          //[FromQuery] string productIds = null,
          [FromQuery] string title = null)
        {

            var query = new PersonReportQuery(pageIndex, pageSize, userId,
                sortField, sortOrder,
                /*sourceTypeIds*/null, /*productIds*/null, title);
            var response = await _reportRepository.GetPersonReport(query);

            return Ok(response);
        }
    }
}
