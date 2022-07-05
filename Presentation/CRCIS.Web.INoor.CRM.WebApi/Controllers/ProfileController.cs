using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
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
    public class ProfileController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IIdentity _identity;
        public ProfileController(IAdminRepository adminRepository, IIdentity identity)
        {
            _adminRepository = adminRepository;
            _identity = identity;
        }

        [JwtAuthorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var adminId = _identity.GetAdminId();
            var response = await _adminRepository.GetProfileByIdAsync(adminId);
            return Ok(response);
        }
    }
}
