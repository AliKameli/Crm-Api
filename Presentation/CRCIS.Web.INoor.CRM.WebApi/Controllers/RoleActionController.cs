using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction.Commands;
using CRCIS.Web.INoor.CRM.WebApi.Models.RoleAction;
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
    public class RoleActionController : ControllerBase
    {
        private readonly IRoleActionRepository _roleActionRepository;

        public RoleActionController(IRoleActionRepository roleActionRepository)
        {
            _roleActionRepository = roleActionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int roleId)
        {
            var response = await _roleActionRepository.GetActionsInRoleAsync(roleId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoleActionUpdateModel model)
        {
            var command = new RoleActionUpdateCommand(model.RoleId, model.ActionIds);
            var response =await _roleActionRepository.UpdateRoleActionAsync(command);
            return Ok(response);
        }
    }
}
