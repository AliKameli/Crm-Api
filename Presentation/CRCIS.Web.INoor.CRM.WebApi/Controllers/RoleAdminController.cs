using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.RoleAdmin;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
using CRCIS.Web.INoor.CRM.WebApi.Models.RoleAdmin;
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
    [JwtAuthorize]
    public class RoleAdminController : ControllerBase
    {
        private readonly IRoleAdminRepository _roleAdminRepository;
        public RoleAdminController(IRoleAdminRepository roleAdminRepository)
        {
            _roleAdminRepository = roleAdminRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int adminId)
        {
            var response = await _roleAdminRepository.GetRolesInAdmin(adminId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoleAdminUpdateModel model)
        {
            var command = new RoleAdminUpdateCommand(model.AdminId, model.RoleIds);
            var response = await _roleAdminRepository.UpdatAdminRolesAsync(command);
            return Ok(response);
        }
    }
}
