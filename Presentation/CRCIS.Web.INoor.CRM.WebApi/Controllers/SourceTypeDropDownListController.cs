using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
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
    public class SourceTypeDropDownListController : ControllerBase
    {
        private readonly ISourceTypeRepository _sourceTypeRepository;
        public SourceTypeDropDownListController(ISourceTypeRepository sourceTypeRepository)
        {
            _sourceTypeRepository = sourceTypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _sourceTypeRepository.GetDropDownListAsync();
            return Ok(response);
        }

    }
}
