using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
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
    public class MoveCasePendingToArchiveMultiController : ControllerBase
    {
        private readonly IPendingCaseService _pendingService;
        private readonly IIdentity _identity;
        public MoveCasePendingToArchiveMultiController(IPendingCaseService pendingService, IIdentity identity)
        {
            _pendingService = pendingService;
            _identity = identity;
        }

        [Authorize]
        [HttpPut]
        public async Task <IActionResult>Put(MoveMultiCaseToArchiveModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToArchiveMultiCommand(adminId, model.CaseIds);
            var response = await _pendingService.MoveCaseToArchiveAsync(command);
            return Ok(response);
        }
    }
}
