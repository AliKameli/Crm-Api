using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.WebApi.Models.UpdateCaseSubject;
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
    public class UpdateCaseRemoveSubjectController : ControllerBase
    {
        private readonly ICaseSubjectService _caseSubjectService;
        private readonly IMapper _mapper;
        public UpdateCaseRemoveSubjectController(ICaseSubjectService caseSubjectService, IMapper mapper)
        {
            _caseSubjectService = caseSubjectService;
            _mapper = mapper;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UpdateCaseRemoveSubjectModel model)
        {
            var command = _mapper.Map<UpdateCaseRemoveSubjectCommand>(model);
            var response =await _caseSubjectService.RemoveSubjectAsync(command);
            return Ok(response);
        }
    }
}
