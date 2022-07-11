using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.WebApi.Models.Warning;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthorize]
    public class WarningMarkAsVisitedController : ControllerBase
    {
        private readonly IWarningService _warningService;

        public WarningMarkAsVisitedController(IWarningService warningService)
        {
            _warningService = warningService;
        }

        [HttpPut]
        public async Task<IActionResult> Put(WarningUpdateMarkVisitedModel model)
        {
            var response = await _warningService.UpdateWarningAsVisitedAsync(model.WarningId);
            return Ok(response);
        }
    }
}
