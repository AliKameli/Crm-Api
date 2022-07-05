using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using System.Security.Principal;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveCaseNewToCurrentAdminCardboardMultiController : ControllerBase
    {
        private readonly IIdentity _identity;
        private readonly ICaseNewService _caseNewService;
        public MoveCaseNewToCurrentAdminCardboardMultiController(IIdentity identity, ICaseNewService caseNewService)
        {
            _identity = identity;
            _caseNewService = caseNewService;
        }

        [JwtAuthorize]
        [HttpPut]
        public async Task<IActionResult> Put(MoveMultiCaseToCurrentAdminCardboardModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToCurrentAdminCardboardMultiCommand(adminId, model.CaseIds);
            var resposne = await _caseNewService.MoveCaseToAdminAsync(command);
            return Ok(resposne);
        }
    }
}