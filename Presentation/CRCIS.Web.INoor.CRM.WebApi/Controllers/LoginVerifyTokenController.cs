using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IPermissionService _permissionService;
        private readonly ILogger _logger;
        public LoginVerifyTokenController(IAdminVerifyTokenRepository adminVerifyTokenRepository,
            IJwtProvider jwtProvider, ITokenStoreService tokenStoreService,
            ILoggerFactory loggerFactory, IPermissionService permissionService)
        {
            _adminVerifyTokenRepository = adminVerifyTokenRepository;
            _jwtProvider = jwtProvider;
            _tokenStoreService = tokenStoreService;
            _logger = loggerFactory.CreateLogger<LoginVerifyTokenController>();
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginVerifyTokenModel verifyToken)
        {
            _logger.LogCritical($"LoginVerifyTokenModel start : {DateTime.Now} VerifyToken : {verifyToken?.VerifyToken}");
            var responseUser = await _adminVerifyTokenRepository.GetAdminByVerifyToken(Guid.Parse(verifyToken.VerifyToken));
            if (responseUser.Success == false)
            {
                return Ok(responseUser);
            }
            _logger.LogCritical($"VerifyToken founded : {DateTime.Now} VerifyToken : {verifyToken?.VerifyToken}");
            var adminModel = new Domain.Users.Admin.AdminModel
            {
                Id = responseUser.Data.Id,
                Name = responseUser.Data.Name,
                Family = responseUser.Data.Family,
                IsActive = responseUser.Data.IsActive,
                SerialNumber = responseUser.Data.SerialNumber,
                Username = responseUser.Data.Username,
            };
            var (expireDate, responseToken) = await _jwtProvider.GenerateAsync(adminModel);
            var resposnePermissions = await _permissionService.GetPermissionsByAdminIdAsync(adminModel.Id);
            var accessTokenData = new AccessTokenData
            {
                ExpireAtUtc = expireDate,
                ValidToMilliseconds = (expireDate - DateTime.UtcNow).TotalMilliseconds,
                //User = (responseUser.Data.Id == 1 || responseUser.Data.Id == 2) ? "crmAdministrator" : "admin",//hard code permission
                AccessToken = responseToken,
                RedreshToken = Guid.NewGuid().ToString(),
                Action = responseUser.Data.Action,
                jsonData = responseUser.Data.JsonData,
                Admin = responseUser.Data.Family,
                Permissions = resposnePermissions.Data
            };

            _logger.LogCritical($"AccessToken generated : {DateTime.Now} VerifyToken : {verifyToken?.VerifyToken}");

            var responseStore = await _tokenStoreService.CreateAsync(accessTokenData, responseUser.Data.Id);

            _logger.LogCritical($"AccessToken stored : {DateTime.Now} VerifyToken : {verifyToken?.VerifyToken}");

            if (responseStore.Success)
            {
                var result = new DataResponse<AccessTokenData>(accessTokenData);
                return Ok(result);
            }
            else
            {
                _logger.LogCritical($"Login Failed : {DateTime.Now} VerifyToken : {verifyToken?.VerifyToken}");
                var result = new DataResponse<int>(new List<string> { "عملیات لاگین ناموفق بود" });
                return Ok(result);
            }
        }
    }
}
