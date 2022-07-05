using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands;
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
    public class MoveCaseArchiveToCurrentAdminCardboardMultiController : ControllerBase
    {
        private readonly IArchiveCaseService _archiveCaseService;
        private readonly IIdentity _identity;
        public MoveCaseArchiveToCurrentAdminCardboardMultiController(IArchiveCaseService archiveCaseService, IIdentity identity)
        {
            _archiveCaseService = archiveCaseService;
            _identity = identity;
        }

        [JwtAuthorize]
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToCurrentAdminCardboardMultiModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToCurrentAdminCardboardMultiCommand(adminId, model.CaseIds);
            var response =await _archiveCaseService.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
