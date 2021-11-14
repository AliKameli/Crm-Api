//using Dapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class INoorController : ControllerBase
//    {
//        public INoorController()
//        {

//        }
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var cns = "data source=172.16.25.30;initial catalog=CRCIS.Web.INoor.Sales;persist security info=True;User ID=inoor.ir;Password=*******;";
//            var date = DateTime.Now.AddDays(-1);
//            try
//            {
//                using (var cn = new SqlConnection(cns))
//                {
//                    cn.Open();
//                    var data = cn.Query<int>("sp", new {  },
//                        commandType: CommandType.StoredProcedure).FirstOrDefault();

//                    return Ok();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }

//}
