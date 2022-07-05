using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveCaseNewToCurrentAdminCardboardController : ControllerBase
    {
        private readonly ICaseNewService _caseNewService;
        private readonly IIdentity _identity;
        public MoveCaseNewToCurrentAdminCardboardController(ICaseNewService  caseNewService, IIdentity identity)
        {
            _caseNewService = caseNewService;
            _identity = identity;
        }
        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(MoveCaseToCurrentAdminCardboardModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToCurrentAdminCardboardCommand(adminId, model.Id);
            var response = await _caseNewService.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}