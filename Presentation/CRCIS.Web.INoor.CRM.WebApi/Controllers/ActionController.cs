using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Menu;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ActionController : ControllerBase
    {
        private readonly IActionRepository _actionRepository;
        public ActionController(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _actionRepository.GetAsync();
            return Ok(list);
        }
    }
}
