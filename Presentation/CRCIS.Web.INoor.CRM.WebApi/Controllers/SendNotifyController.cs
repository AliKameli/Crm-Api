//using CRCIS.Web.INoor.CRM.Contract.Notifications;
//using CRCIS.Web.INoor.CRM.WebApi.Models.Notify;
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
//    public class SendNotifyController : ControllerBase
//    {
//        private readonly ICrmNotifyManager _crmNotifyManager;
//        public SendNotifyController(ICrmNotifyManager crmNotifyManager)
//        {
//            _crmNotifyManager = crmNotifyManager;
//        }
//        [HttpPost]
//        public async Task<IActionResult> Post(SendNotifyCreateModel model)
//        {
//            var response = await _crmNotifyManager.SendEmailAsync(model.caseId,model.FromMail,model.MessageBody);
//            return Ok(response);
//        }
//    }
//}
