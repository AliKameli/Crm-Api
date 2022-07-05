using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.WebApi.Models.Subject;
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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _subjectRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            var query = new SubjectDataTableQuery(pageIndex, pageSize);
            var response = await _subjectRepository.GetAsync(query);

            return Ok(response);
        }
        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Post(SubjectCreateModel model)
        {
            var command = _mapper.Map<SubjectCreateCommand>(model);
            var response = await _subjectRepository.CreateAsync(command);
            return Ok(response);
        }
        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(SubjectUpdateModel model)
        {
            var command = _mapper.Map<SubjectUpdateCommand>(model);
            var response = await _subjectRepository.UpdateAsync(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _subjectRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}
