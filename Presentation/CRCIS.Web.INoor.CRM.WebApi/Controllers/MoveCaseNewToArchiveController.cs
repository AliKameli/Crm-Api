using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
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
    public class MoveCaseNewToArchiveController : ControllerBase
    {
        private readonly ICaseNewService _caseNewService;
        public MoveCaseNewToArchiveController(ICaseNewService caseNewService)
        {
            _caseNewService = caseNewService;
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(MoveCaseToArchiveModel model)
        {
            var command = new MoveCaseToArchiveCommand(model.Id);

            var response = await _caseNewService.MoveCaseToArchive(command);
            return Ok(response);
        }
    }
}
