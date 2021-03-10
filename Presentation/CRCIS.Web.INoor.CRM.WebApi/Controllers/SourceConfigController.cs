using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.SourceConfig;
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
    public class SourceConfigController : ControllerBase
    {
        private readonly ISourceConfigRepository _sourceConfigRepository;
        private readonly IMapper _mapper;
        public SourceConfigController(ISourceConfigRepository sourceConfigRepository, IMapper mapper)
        {
            _sourceConfigRepository = sourceConfigRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _sourceConfigRepository.GetByIdAsync(id);

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            var query = new SourceConfigDataTableQuery(pageIndex, pageSize);
            var response = await _sourceConfigRepository.GetAsync(query);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(SourceConfigCreateModel model)
        {

            var command = _mapper.Map<SourceConfigCreateCommand>(model);
            var response = await _sourceConfigRepository.CreateAsync(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(SourceConfigUpdateModel model)
        {
            var command = _mapper.Map<SourceConfigUpdateCommand>(model);
            var response = await _sourceConfigRepository.UpdateAsync(command);

            return Ok(response);
        }
    }
}
