using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.WebApi.Models.CaseSubject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseSubjectController : ControllerBase
    {
        private readonly ICaseSubjectRepository _caseSubjectRepository;
        private readonly IMapper _mapper;
        public CaseSubjectController(ICaseSubjectRepository caseSubjectRepository, IMapper mapper)
        {
            _caseSubjectRepository = caseSubjectRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _caseSubjectRepository.GetByIdAsync(id);

            return Ok(response);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] int pageSize,
        //  [FromQuery] int pageIndex,
        //  [FromQuery] string sortField,
        //  [FromQuery] SortOrder? sortOrder)
        //{
        //    var query = new CaseSubjectDataTableQuery(pageIndex, pageSize);
        //    var response = await _caseSubjectRepository.GetAsync(query);

        //    return Ok(response);
        //}

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Post(CaseSubjectCreateModel model)
        {
            var command = _mapper.Map<CaseSubjectCreateCommand>(model);
            var response = await _caseSubjectRepository.CreateAsync(command);
            return Ok(response);
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(CaseSubjectUpdateModel model)
        {
            var command = _mapper.Map<CaseSubjectUpdateCommand>(model);
            var response = await _caseSubjectRepository.UpdateAsync(command);
            return Ok(response);
        }
    }
}
