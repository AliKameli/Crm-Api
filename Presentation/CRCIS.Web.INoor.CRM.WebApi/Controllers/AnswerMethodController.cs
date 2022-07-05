using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Queris;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.AnswerMethod;
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
    public class AnswerMethodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnswerMethodRepository _answerMethodRepository;
        public AnswerMethodController(IAnswerMethodRepository answerMethodRepository, IMapper mapper)
        {
            _answerMethodRepository = answerMethodRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _answerMethodRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new AnswerMethodDataTableQuery(pageIndex, pageSize,sortField,sortOrder);
            var response = await _answerMethodRepository.GetAsync(query);

            return Ok(response);
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Post(AnswerMethodCreateModel model)
        {
            var command = _mapper.Map<AnswerMethodCreateCommand>(model);
            var response = await _answerMethodRepository.CreateAsync(command);
            return Ok(response);
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> Put(AnswerMethodUpdateModel model)
        {
            var command = _mapper.Map<AnswerMethodUpdateCommand>(model);
            var response = await _answerMethodRepository.UpdateAsync(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _answerMethodRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}