//using CRCIS.Web.INoor.CRM.Contract.Notifications;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TestSmsController : ControllerBase
//    {
//        private readonly ISmsService _smsService;
//        public TestSmsController(ISmsService smsService)
//        {
//            _smsService = smsService;
//        }
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var request = new SmsRequest { Destination = "+989104633524", Body = "تست ارسال پیامک" };
//            var res = await _smsService.SendSmsAsync(request);
//            return Ok(res);
//        }
//    }
//}
