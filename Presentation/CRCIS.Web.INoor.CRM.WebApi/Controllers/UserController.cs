using CRCIS.Web.INoor.CRM.Contract.ElkSearch;
using CRCIS.Web.INoor.CRM.Domain.Users.UserSearch;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Attributes;
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
        private readonly IUserSearch _userSearch;
        public UserController(IUserSearch userSearch)
        {
            _userSearch = userSearch;
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get(string username, string fullname,string email , string mobile)
        {
            //var model = new SearchUserGlobalSearchInputDto { SearchText = search };
            //var client = new ApiHttpClient();
            //var data = await client.GetUsersGlobalSearchAsync(model);

            var data = await _userSearch.SearchAsync(fullname, mobile, username, email);

            var datatableresponse = new DataTableResponse<List<SeadrchUserDto>>(data.Data, data.TotalCount);
            return Ok(datatableresponse);
        }
    }
}
