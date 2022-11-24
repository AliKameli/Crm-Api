using CRCIS.Web.INoor.CRM.Contract.ElkSearch;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries;
using CRCIS.Web.INoor.CRM.Domain.Users.UserSearch;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportPersonByMobileViopController : ControllerBase
    {
        private readonly IUserSearch _userSearch;

        public ReportPersonByMobileViopController(IUserSearch userSearch)
        {
            _userSearch = userSearch;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string mobile, [FromQuery] string sortField = null, [FromQuery] SortOrder? sortOrder = null)
        {
            var data = await _userSearch.SearchAsync("", mobile, "", "");

            var query = new PersonByMobileReportQuery(1, 99999, sortField, sortOrder, mobile); ;



            var datatableresponse = new DataTableResponse<List<SeadrchUserDto>>(data.Data, data.TotalCount);
            return Ok(datatableresponse);
        }
    }
}
