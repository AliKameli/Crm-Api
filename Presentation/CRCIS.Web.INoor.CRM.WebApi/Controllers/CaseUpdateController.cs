using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
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
    public class CaseUpdateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPendingCaseService _pendingCaseService ;
        public CaseUpdateController(IMapper mapper, IPendingCaseService pendingCaseService )
        {
            _mapper = mapper;
            _pendingCaseService = pendingCaseService;
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(CaseUpdateModel model)
        {
            var command = _mapper.Map<PendingCaseUpdateCommand>(model);
            var response = await _pendingCaseService.UpdateCaseAsync(command);
            return Ok(response);
        }
    }
}