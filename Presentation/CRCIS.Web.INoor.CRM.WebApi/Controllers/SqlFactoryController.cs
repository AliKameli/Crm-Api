using CRCIS.Web.INoor.CRM.Data.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlFactoryController : ControllerBase
    {
        ISqlConnectionFactory _sqlConnectionFactory;

        public SqlFactoryController(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var database = _sqlConnectionFactory.GetOpenConnection().Database;
            return Content(database);
        }
    }
}
