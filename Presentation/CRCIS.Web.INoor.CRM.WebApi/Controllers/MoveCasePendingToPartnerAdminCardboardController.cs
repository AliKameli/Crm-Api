using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using Microsoft.AspNetCore.Authorization;
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
    public class MoveCasePendingToPartnerAdminCardboardController : ControllerBase
    {
        private readonly IPendingCaseService _pendingCaseService;
        public MoveCasePendingToPartnerAdminCardboardController(IPendingCaseService pendingCaseService)
        {
            _pendingCaseService = pendingCaseService;
        }
        [JwtAuthorize]
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToPartnerAdminCardboardModel model)
        {
            var command = new MoveCaseToPartnerAdminCardboardCommand(model.AdminId, model.Id);
            var response = await _pendingCaseService.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
