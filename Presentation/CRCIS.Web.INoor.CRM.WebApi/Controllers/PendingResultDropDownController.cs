using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
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
    public class PendingResultDropDownController : ControllerBase
    {
        public PendingResultDropDownController()
        {

        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = new List<PendingResultDto>
            {
                new PendingResultDto{Id = 1, Title ="در صف ارسال قرار گرفته است"},
                new PendingResultDto{Id = 2, Title ="با موفقیت ارسال شده است"},
                new PendingResultDto{Id = 3, Title ="ارسال نشده است"},
            };

            var response = new DataTableResponse<IEnumerable<PendingResultDto>>(data, data.Count);

            return Ok(response);
        }
    }
}
