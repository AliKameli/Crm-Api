using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
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
    public class AnswerMethodDropDownListController : ControllerBase
    {
        private readonly IAnswerMethodRepository _answerMethodRepository;

        public AnswerMethodDropDownListController(IAnswerMethodRepository answerMethodRepository)
        {
            _answerMethodRepository = answerMethodRepository;

        }
        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get()
        {
            var response = await _answerMethodRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
