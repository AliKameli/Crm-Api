using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Queries;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
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
    public class CaseNewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImportCaseRepository _importCaseRepository;
        public CaseNewController(IMapper mapper, IImportCaseRepository importCaseRepository)
        {
            _mapper = mapper;
            _importCaseRepository = importCaseRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
          [FromQuery] int pageIndex,
          [FromQuery] string sortField,
          [FromQuery] SortOrder? sortOrder)
        {
            var query = new ImportCaseDataTableQuery(pageIndex, pageSize, sortField, sortOrder);
            var response = await _importCaseRepository.GetAsync(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CaseNewCreateModel model)
        {
            model.ManualImportAdminId = 1;
            var command = _mapper.Map<ImportCaseCreateCommand>(model);
            var response = await _importCaseRepository.CreateAsync(command,model.SubjectIds);
            return Ok(response);
        }
    }
}
