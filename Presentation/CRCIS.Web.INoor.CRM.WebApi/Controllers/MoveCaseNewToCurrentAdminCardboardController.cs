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
    public class MoveCaseNewToCurrentAdminCardboardController : ControllerBase
    {
        private readonly IImportCaseRepository _importCaseRepository;
        public MoveCaseNewToCurrentAdminCardboardController(IImportCaseRepository importCaseRepository)
        {
            _importCaseRepository = importCaseRepository;
        }
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToCurrentAdminCardboardModel model)
        {
            var adminId = 1;
            var command = new MoveCaseToCurrentAdminCardboardCommand(adminId, model.Id);

            var response = await _importCaseRepository.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
