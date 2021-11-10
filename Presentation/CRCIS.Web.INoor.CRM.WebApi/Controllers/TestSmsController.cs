using CRCIS.Web.INoor.CRM.Contract.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSmsController : ControllerBase
    {
        private readonly ISmsService _smsService;
        public TestSmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string number)
        {
            var message = new SmsRequest
            {
                Destination = number,
                Body = "تست ارسال پیامک",
                SmsCenterPanelNumber = "3000144194",
                SmsCenterUserName = "noornet3",
                SmsCenterPassword = "tsms7665ef5",
            };
            await _smsService.SendSmsAsync(message).ConfigureAwait(false);

            //var res = await _smsService.SendSmsAsync(request);
            //var ocRequest = System.Net.WebRequest.Create(url);
            //ocRequest.Timeout = 30000;
            //var res = ocRequest.GetResponse();

            //var url =
            //   "http://www.tsms.ir/url/tsmshttp.php?" +
            //      $"from={message.SmsCenterPanelNumber}" +
            //      $"&username={message.SmsCenterUserName}" +
            //      $"&password={message.SmsCenterPassword}" +
            //      $"&to={message.Destination}" +
            //      $"&message={message.Body}";
            //var handler = new HttpClientHandler
            //{
            //    Proxy = new WebProxy()
            //    {
            //        Address = new Uri("http://172.16.20.207:3128"),
            //        BypassProxyOnLocal = true,
            //        UseDefaultCredentials = false,
            //    }
            //};

            //using var httpClient = new HttpClient(handler);

            //var resposne = await httpClient.GetAsync(url);

            //var str = resposne.Content.ReadAsStringAsync();
            //var r = new
            //{
            //    resposne.StatusCode,
            //    Content = str,
            //};
            return Ok();
        }
    }
}
