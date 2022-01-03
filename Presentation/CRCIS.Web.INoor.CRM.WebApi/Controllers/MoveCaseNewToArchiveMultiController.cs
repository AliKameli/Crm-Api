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
    public class MoveCaseNewToArchiveMultiController : ControllerBase
    {
        private readonly ICaseNewService _caseNewService;
        private readonly IIdentity _identity;
        public MoveCaseNewToArchiveMultiController(ICaseNewService caseNewService,IIdentity  identity)
        {
            _caseNewService = caseNewService;
            _identity = identity;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(MoveMultiCaseToArchiveModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToArchiveMultiCommand(adminId, model.CaseIds);
            var resposne = await _caseNewService.MoveCaseToArchive(command);
            return Ok(resposne);
        }
    }
}
