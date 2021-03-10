using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveCasePendingToArchiveController : ControllerBase
    {
        private readonly IPendingCaseRepository _pendingCaseRepository;
        public MoveCasePendingToArchiveController(IPendingCaseRepository pendingCaseRepository)
        {
            _pendingCaseRepository = pendingCaseRepository;
        }
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToArchiveModel model)
        {
            var command = new MoveCaseToArchiveCommand(model.Id);

            var response = await _pendingCaseRepository.MoveCaseToArchive(command);
            return Ok(response);
        }
    }
}
