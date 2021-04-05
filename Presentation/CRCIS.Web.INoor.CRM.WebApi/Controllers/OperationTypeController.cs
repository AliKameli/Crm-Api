using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.OpreationType;
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
    public class OperationTypeController : ControllerBase
    {
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly IMapper _mapper;
        public OperationTypeController(IOperationTypeRepository operationTypeRepository, IMapper mapper)
        {
            _operationTypeRepository = operationTypeRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _operationTypeRepository.GetByIdAsync(id);

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new OperationTypeDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var response = await _operationTypeRepository.GetAsync(query);

            return Ok(response);
        }
        //[HttpPost]
        //public async Task<IActionResult> Post(OperationTypeCreateModel model)
        //{

        //    var command = _mapper.Map<OperationTypeCreateCommand>(model);
        //    var response = await _operationTypeRepository.CreateAsync(command);
        //    return Ok(response);
        //}
        //[HttpPut]
        //public async Task<IActionResult> Put(OperationTypeUpdateModel model)
        //{
        //    var command = _mapper.Map<OperationTypeUpdateCommand>(model);
        //    var response = await _operationTypeRepository.UpdateAsync(command);

        //    return Ok(response);
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _operationTypeRepository.DeleteAsync(id);
        //    return Ok(response);
        //}
    }
}
