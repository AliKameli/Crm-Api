using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.WebApi.Models.AdminNoor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public RequestVerifyTokenController(IAdminService adminService, ILoggerFactory loggerFactory)
        {
            _adminService = adminService;
            _logger = loggerFactory.CreateLogger<RequestVerifyTokenController>();
        }
        [HttpPost]
        public async Task<IActionResult> Post(RequestVerifyTokenModel model)
        {
            _logger.LogCritical($"RequestVerifyTokenModel start : {DateTime.Now} PersonId : {model?.PersonId}");

            var dataResponse = await _adminService.GetVerifyTokenForNoorAdmin(model.Username, model.Name, model.Family, model.PersonId, model.Action, model.QueryString);

            _logger.LogCritical($"RequestVerifyTokenModel responsed : {DateTime.Now} PersonId : {model?.PersonId}");
            return Ok(dataResponse);
        }
    }
}
