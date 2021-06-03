using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.WebApi.Models.AdminNoor;
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
    public class RequestVerifyTokenController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public RequestVerifyTokenController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(RequestVerifyTokenModel model)
        {
            var dataResponse = await _adminService.GetVerifyTokenForNoorAdmin(model.Username, model.Name, model.Family, model.PersonId, model.Action, model.QueryString);
            return Ok(dataResponse);
        }
    }
}
