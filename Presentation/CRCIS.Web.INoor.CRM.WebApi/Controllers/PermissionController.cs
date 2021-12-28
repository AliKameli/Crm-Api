using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
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
    public class PermissionController : ControllerBase
    {

        private readonly IAdminRepository _adminRepository;
        private readonly IIdentity _identity;
        public PermissionController(IAdminRepository adminRepository, IIdentity identity)
        {
            _adminRepository = adminRepository;
            _identity = identity;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var adminId = _identity.GetAdminId();
            var responseUser = await _adminRepository.GetProfileByIdAsync(adminId);
            if (responseUser == null || responseUser.Success == false)
            {
                return BadRequest();
            }
            var permission = (responseUser.Data.Id == 1 || responseUser.Data.Id == 2||responseUser.Data.Id == 8) ? "crmAdministrator" : "admin";//hard code permission
            var response = new DataResponse<string>(permission);
            return Ok(response);
        }
    }
}
