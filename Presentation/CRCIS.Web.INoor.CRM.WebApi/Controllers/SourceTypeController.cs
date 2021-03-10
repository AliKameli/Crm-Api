using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Queries;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.SourceType;
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
    public class SourceTypeController : ControllerBase
    {
        private readonly ISourceTypeRepository _sourceTypeRepository;
        private readonly IMapper _mapper;

        public SourceTypeController(ISourceTypeRepository sourceTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _sourceTypeRepository = sourceTypeRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _sourceTypeRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var dataTable = new SourceTypeDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var response = await _sourceTypeRepository.GetAsync(dataTable);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(SourceTypeCreateModel model)
        {
            var command = _mapper.Map<SourceTypeCreateCommand>(model);
            var resposne = await _sourceTypeRepository.CreateAsync(command);
            return Ok(resposne);
        }
        [HttpPut]
        public async Task<IActionResult> Put(SourceTypeUpdateModel model)
        {
            var command = _mapper.Map<SourceTypeUpdateCommand>(model);
            var response = await _sourceTypeRepository.UpdateAsync(command);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _sourceTypeRepository.DeleteAsync(id);
            return Ok(response);
        }
    }
}
