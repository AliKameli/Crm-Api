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
    public class MoveCasePendingToPartnerAdminCardboardMultiController : ControllerBase
    {
        private readonly IPendingCaseService _pendingService;
        public MoveCasePendingToPartnerAdminCardboardMultiController(IPendingCaseService pendingCaseService)
        {
            _pendingService = pendingCaseService;
        }

        [JwtAuthorize]
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToPartnerAdminCardboardMultiModel model)
        {
            var command = new MoveCaseToPartnerAdminCardboardMultiCommand(model.CaseIds, model.AdminId);
            var response = await _pendingService.MoveCaseToAdminAsync(command);
            return Ok(response);

        }
    }
}
