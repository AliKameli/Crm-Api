using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        private readonly ITokenStoreService _tokenStoreService;
        public LoginController(IAdminRepository adminRepository, IMapper mapper, IJwtProvider jwtProvider, ITokenStoreService tokenStoreService)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _tokenStoreService = tokenStoreService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginModel model)
        {
            var query = _mapper.Map<AdminLoginQuery>(model);
            var responseUser = await _adminRepository.FindAdminAsync(query);

            if (responseUser.Success == false)
            {
                return Ok(responseUser);
            }

            var (expireDate, responseToken) = await _jwtProvider.GenerateAsync(responseUser.Data);
            var accessTokenData = new AccessTokenData
            {
                ExpireAtUtc = expireDate,
                AccessToken = responseToken,
                RedreshToken = Guid.NewGuid().ToString()
            };

            var responseStore = await _tokenStoreService.CreateAsync(accessTokenData, responseUser.Data.Id);

            if (responseStore.Success)
            {
                var result = new DataResponse<AccessTokenData>(accessTokenData);
                return Ok(result);
            }
            else
            {
                var result = new DataResponse<int>(new List<string> { "عملیات لاگین ناموفق بود"});
                return Ok(result);
            }
        }
    }
}
