using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries;
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
    public class AdminPendingCaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPendingCaseRepository _pendingCaseRepository;
        private readonly IImportCaseRepository _importCaseRepository;
        public AdminPendingCaseController(IMapper mapper, IPendingCaseRepository pendingCaseRepository, IImportCaseRepository importCaseRepository)
        {
            _mapper = mapper;
            _pendingCaseRepository = pendingCaseRepository;
            _importCaseRepository = importCaseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize,
            [FromQuery] int pageIndex,
            [FromQuery] string sortField,
            [FromQuery] SortOrder? sortOrder)
        {
            var adminId = 1;
            var query = new AdminPendingCaseDataTableQuery(pageIndex, pageSize, sortField, sortOrder, adminId);
            var response = await _pendingCaseRepository.GetForAdminAsync(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CaseNewCreateModel model)
        {
            var adminId = 1;
            model.ManualImportAdminId =adminId;
          

            var command = _mapper.Map<ImportCaseCreateCommand>(model);
            var response = await _importCaseRepository.CreateAndMoveToAdmin(command,adminId,model.SubjectIds);
            return Ok(response);
        }
    }
}