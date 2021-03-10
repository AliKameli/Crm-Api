using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.CommonAnswer;
using Microsoft.AspNetCore.Cors;
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
    public class CommonAnswerController : ControllerBase
    {
        private readonly ICommonAnswerRepository _commonAnswerRepository;
        private readonly IMapper _mapper;
        public CommonAnswerController(IMapper mapper, ICommonAnswerRepository commonAnswerRepository)
        {
            _mapper = mapper;
            _commonAnswerRepository = commonAnswerRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _commonAnswerRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string searchWord,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new CommonAnswerDataTableQuery(pageIndex, pageSize, searchWord, sortField, sortOrder);
            var response = await _commonAnswerRepository.GetAsync(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommonAnswerCreateModel model)
        {
            var command = _mapper.Map<CommonAnswerCreateCommand>(model);
            var response = await _commonAnswerRepository.CreateAsync(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CommonAnswerUpadateModel model)
        {
            var command = _mapper.Map<CommonAnswerUpdateCommand>(model);
            var response = await _commonAnswerRepository.UpdateAsync(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _commonAnswerRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}
