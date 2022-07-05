using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands;
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
    public class MoveCaseArchiveToCurrentAdminCardboardController : ControllerBase
    {
        private readonly IArchiveCaseService _archiveCaseService ;
        private readonly IIdentity _identity;
        public MoveCaseArchiveToCurrentAdminCardboardController(IArchiveCaseService   archiveCaseService, IIdentity identity)
        {
            _archiveCaseService = archiveCaseService;
            _identity = identity;
        }
        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(MoveCaseToCurrentAdminCardboardModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToCurrentAdminCardboardCommand(adminId, model.Id);

            var response = await _archiveCaseService.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
