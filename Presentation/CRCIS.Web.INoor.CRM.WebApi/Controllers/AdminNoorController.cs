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
    public class AdminNoorController : ControllerBase
    {
        public AdminNoorController()
        {

        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
