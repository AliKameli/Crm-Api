using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
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
        private readonly IArchiveCaseRepository _archiveCaseRepository;
        private readonly IIdentity _identity;
        public MoveCaseArchiveToCurrentAdminCardboardController(IArchiveCaseRepository archiveCaseRepository, IIdentity identity)
        {
            _archiveCaseRepository = archiveCaseRepository;
            _identity = identity;
        }
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToCurrentAdminCardboardModel model)
        {
            var adminId = _identity.GetAdminId();
            var command = new MoveCaseToCurrentAdminCardboardCommand(adminId, model.Id);

            var response = await _archiveCaseRepository.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
