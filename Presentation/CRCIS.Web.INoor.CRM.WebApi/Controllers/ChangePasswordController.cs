using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.WebApi.Models.Panel;
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
    public class ChangePasswordController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public ChangePasswordController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPut]
        public async Task<IActionResult> Put(ChangePasswordModel model)
        {
            var response = await _adminService.ChangePasswordAsync(model.OldPassword, model.NewPassword);
            return Ok(response);
        }
    }
}
