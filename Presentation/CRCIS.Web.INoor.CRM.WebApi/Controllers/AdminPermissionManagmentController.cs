using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.AdminAction;
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
    public class AdminPermissionManagmentController : ControllerBase
    {
        private readonly IAdminActionRepository _adminActionRepository;
        public AdminPermissionManagmentController(IAdminActionRepository adminActionRepository)
        {
            _adminActionRepository = adminActionRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var list = await _adminActionRepository.GetAdminActionByAdminIdAsync(id);
            return Ok(list);
        }
    }
}
