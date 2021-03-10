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
    public class MoveCaseArchiveToCurrentAdminCardboardController : ControllerBase
    {
        private readonly IArchiveCaseRepository _archiveCaseRepository;
        public MoveCaseArchiveToCurrentAdminCardboardController(IArchiveCaseRepository archiveCaseRepository)
        {
            _archiveCaseRepository = archiveCaseRepository;
        }
        [HttpPut]
        public async Task<IActionResult> Put(MoveCaseToCurrentAdminCardboardModel model)
        {
            var adminId = 1;
            var command = new MoveCaseToCurrentAdminCardboardCommand(adminId, model.Id);

            var response = await _archiveCaseRepository.MoveCaseToAdminAsync(command);
            return Ok(response);
        }
    }
}
