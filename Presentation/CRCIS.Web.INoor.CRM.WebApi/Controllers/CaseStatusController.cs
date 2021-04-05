using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.CaseStatus;
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
    public class CaseStatusController : ControllerBase
    {

        private readonly ICaseStatusRepository _caseStatusRepository;
        private readonly IMapper _mapper;
        public CaseStatusController(ICaseStatusRepository caseStatusRepository, IMapper mapper)
        {
            _caseStatusRepository = caseStatusRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _caseStatusRepository.GetByIdAsync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var query = new CaseStatusDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var response = await _caseStatusRepository.GetAsync(query);


            return Ok(response);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(CaseStatusCreateModel model)
        //{

        //    var command = _mapper.Map<CaseStatusCreateCommand>(model);
        //    var response = await _caseStatusRepository.CreateAsync(command);
        //    return Ok(response);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Put(CaseStatusUpdateModel model)
        //{
        //    var command = _mapper.Map<CaseStatusUpdateCommand>(model);
        //    var response = await _caseStatusRepository.UpdateAsync(command);

        //    return Ok(response);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _caseStatusRepository.DeleteAsync(id);
        //    return Ok(response);
        //}
    }
}
