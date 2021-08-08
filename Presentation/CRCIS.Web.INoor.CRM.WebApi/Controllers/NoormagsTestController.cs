using CRCIS.Web.INoor.CRM.Contract.Notifications;
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
    public class NoormagsTestController : ControllerBase
    {
        private readonly IMailService _mailService;
        public NoormagsTestController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var mailRequest = new MailRequest
            {
                ToEmail = "Mabdollahi@noornet.net",
                Subject = "تست ارسال crm",
                Body = "تست برنامه نویس"
            };
            var mailSettings = new MailSettings
            {
                UserName = "support.noormags.com",
                Mail = "support.noormags.com",
                Host = "support.noormags.com",
                Port = 465

            };
          await  _mailService.SendEmailAsync(mailRequest, mailSettings);

            return Ok();
        }
    }
}
