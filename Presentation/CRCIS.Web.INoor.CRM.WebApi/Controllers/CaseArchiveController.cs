using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Queris;
using CRCIS.Web.INoor.CRM.Utility.Queries;
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
    public class CaseArchiveController : ControllerBase
    {
        private readonly IArchiveCaseRepository _archiveCaseRepository;
        public CaseArchiveController(IArchiveCaseRepository archiveCaseRepository)
        {
            _archiveCaseRepository = archiveCaseRepository;
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
            var query = new ArchiveCaseDataTableQuery(pageIndex, pageSize, sortField, sortOrder, sourceTypeTitle,productTitle, title);
            var response = await _archiveCaseRepository.GetAsync(query);

            return Ok(response);
        }
    }
}