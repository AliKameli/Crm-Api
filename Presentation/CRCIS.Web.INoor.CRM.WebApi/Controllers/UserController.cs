using CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient;
using CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient.Dtos.SearchUserDto;
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
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {
            if (search is null) search = "م*";
            else search = $"${search}*";
            var model = new SearchUserGlobalSearchInputDto { SearchText = search };
            var client = new ApiHttpClient();
            var data =await client.GetUsersGlobalSearchAsync(model);
      
            var count = data == null ? 0 : data.Count();
            var datatableresponse = new DataTableResponse<IEnumerable<RedisUserDto>>(data,count);
            return Ok(datatableresponse);
        }
    }
}
