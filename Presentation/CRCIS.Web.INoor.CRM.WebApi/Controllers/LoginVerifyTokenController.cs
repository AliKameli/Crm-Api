using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.Account;
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
    public class LoginVerifyTokenController : ControllerBase
    {
        private readonly IAdminVerifyTokenRepository _adminVerifyTokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ITokenStoreService _tokenStoreService;
        public LoginVerifyTokenController(IAdminVerifyTokenRepository adminVerifyTokenRepository, IJwtProvider jwtProvider, ITokenStoreService tokenStoreService)
        {
            _adminVerifyTokenRepository = adminVerifyTokenRepository;
            _jwtProvider = jwtProvider;
            _tokenStoreService = tokenStoreService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginVerifyTokenModel verifyToken)
        {
            var responseUser = await _adminVerifyTokenRepository.GetAdminByVerifyToken(Guid.Parse(verifyToken.VerifyToken));
            if (responseUser.Success == false)
            {
                return Ok(responseUser);
            }

            var (expireDate, responseToken) = await _jwtProvider.GenerateAsync(responseUser.Data);
            var accessTokenData = new AccessTokenData
            {
                ExpireAtUtc = expireDate,
                ValidToMilliseconds = (expireDate - DateTime.UtcNow).TotalMilliseconds,
                User = responseUser.Data.Id == 1 ? "crmAdministrator" : "admin",//hard code permission
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
                var result = new DataResponse<int>(new List<string> { "عملیات لاگین ناموفق بود" });
                return Ok(result);
            }
        }
    }
}
