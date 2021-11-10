using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.WebApi.Models.Case;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CaseNewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;
        private readonly IImportCaseRepository _importCaseRepository;
        public CaseNewController(IMapper mapper, IImportCaseRepository importCaseRepository,
            IIdentity identity)
        {
            _mapper = mapper;
            _importCaseRepository = importCaseRepository;
            _identity = identity;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
          [FromQuery] int pageIndex,
          [FromQuery] string sortField,
          [FromQuery] SortOrder? sortOrder,
          [FromQuery] string sourceTypeTitle = null,
          [FromQuery] string productTitle = null,
          [FromQuery] string title = null)
        {
            var query = new ImportCaseDataTableQuery(pageIndex, pageSize, sortField, sortOrder, sourceTypeTitle,productTitle, title);
            var response = await _importCaseRepository.GetAsync(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CaseNewCreateModel model)
        {
            model.ManualImportAdminId = _identity.GetAdminId();
            var command = _mapper.Map<ImportCaseCreateCommand>(model);
            var response = await _importCaseRepository.CreateAsync(command, model.SubjectIds);
            return Ok(response);
        }
    }
}