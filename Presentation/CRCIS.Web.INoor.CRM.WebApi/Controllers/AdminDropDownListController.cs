using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
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
    public class AdminDropDownListController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        public AdminDropDownListController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get()
        {
            var response = await _adminRepository.GetDropDownListAsync();
            return Ok(response);
        }
    }
}
