using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
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
    public class CommonAnswerDropDownListController : ControllerBase
    {
        private readonly ICommonAnswerRepository _commonAnswerRepository;

        public CommonAnswerDropDownListController(ICommonAnswerRepository commonAnswerRepository)
        {
            _commonAnswerRepository = commonAnswerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _commonAnswerRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
