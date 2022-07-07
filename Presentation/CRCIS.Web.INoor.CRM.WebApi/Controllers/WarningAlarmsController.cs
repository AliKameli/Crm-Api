using CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.Warnings;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[JwtAuthorize]
    public class WarningAlarmsController : ControllerBase
    {

        private readonly IWarningService _warningService;

        public WarningAlarmsController(IWarningService warningService)
        {
            _warningService = warningService;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {

            var response =await _warningService.GetImportantWarningsDayCountAsync();
            return Ok(response);   
        }
    }
}
