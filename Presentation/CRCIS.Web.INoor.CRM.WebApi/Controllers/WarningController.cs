using CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.Warnings;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthorize]
    public class WarningController : ControllerBase
    {
        private readonly IWarningRepository _warningRepository;
        public WarningController(IWarningRepository warningRepository)
        {
            _warningRepository = warningRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex)
        {
            var query = new WarningDataTableQuery(pageIndex, pageSize);
            var response =await _warningRepository.GetAsync(query);
            return Ok(response);
        }
    }
}
