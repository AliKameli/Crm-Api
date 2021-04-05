using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
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
    public class MoveCasePendingToPartnerAdminCardboardController : ControllerBase
    {
        private readonly IPendingCaseRepository _pendingCaseRepository;
        public MoveCasePendingToPartnerAdminCardboardController(IPendingCaseRepository pendingCaseRepository)
        {
            _pendingCaseRepository = pendingCaseRepository;
        }
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToPartnerAdminCardboardModel model)
        {
            var command = new MoveCaseToPartnerAdminCardboardCommand(model.AdminId, model.Id);

            var response = await _pendingCaseRepository.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
