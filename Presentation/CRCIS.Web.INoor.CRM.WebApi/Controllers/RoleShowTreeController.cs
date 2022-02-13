using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role;
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
    public class RoleShowTreeController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleShowTreeController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resposne = await _roleRepository.GetShowTreeAsync();
            return Ok(resposne);
        }
    }
}
