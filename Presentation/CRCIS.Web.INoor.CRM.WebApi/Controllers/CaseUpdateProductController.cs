using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using Microsoft.AspNetCore.Authorization;
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
    public class CaseUpdateProductController : ControllerBase
    {
        private readonly IPendingCaseRepository _pendingCaseRepository;
        private readonly IMapper _mapper;
        public CaseUpdateProductController(IPendingCaseRepository pendingCaseRepository, IMapper mapper)
        {
            _pendingCaseRepository = pendingCaseRepository;
            _mapper = mapper;
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(CaseUpdateProductModel model)
        {
            var command = _mapper.Map<CaseUpdateProductCommand>(model);
            var resposne = await _pendingCaseRepository.UpdateCaseProductAsync(command);
            return Ok(resposne);
        }
    }
}
