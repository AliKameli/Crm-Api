using CRCIS.Web.INoor.CRM.Contract.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoormagsTestController : ControllerBase
    {
        private readonly IMailService _mailService;
        public NoormagsTestController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpGet("{mail}")]
        public async Task<IActionResult> Get(string mail)
        {
            if (string.IsNullOrEmpty(mail))
                return Content("ایمیل مقصد وارد شود");
            var mailRequest = new MailRequest
            {
                ToEmail = mail,
                Subject = "تست ارسال crm",
                Body = "تست برنامه نویس"
            };
            //var mailSettings = new MailSettings
            //{
            //    Mail = "support@inoor.ir",
            //    Host = "mail.noornet.net",
            //    Port = 25,
            //    Password = "@*ldegF2**3MzVJHfsge"

            //};
            var mailSettings = new MailSettings
            {
                Mail = "support@noormags.com",
                Host = "mail.noornet.net",
                Port = 25,
                Password = "aLBsZkFwf3sy#PrKICtS"

            };

            await _mailService.SendEmailAsync(mailRequest, mailSettings);

            return Ok();
        }
    }
}
