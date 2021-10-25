using CRCIS.Web.INoor.CRM.Data.Database;
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
    public class SqlFactoryController : ControllerBase
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SqlFactoryController(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var databas = _sqlConnectionFactory.GetOpenConnection().Database;
            dynamic s = _sqlConnectionFactory.GetOpenConnection();
            var dataSource = s.DataSource;
            return Ok(new { databas,dataSource} );
        }
    }
}
