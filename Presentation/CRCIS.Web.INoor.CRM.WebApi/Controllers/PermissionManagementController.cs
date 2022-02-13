using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionManagementController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IIdentity _identity;
        public PermissionManagementController(IPermissionService permissionService, IIdentity identity)
        {
            _permissionService = permissionService;
            _identity = identity;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resposne = await _permissionService.GetPermissionsByAdminIdAsync(_identity.GetAdminId());
            return Ok(resposne);
        }
    }
}
