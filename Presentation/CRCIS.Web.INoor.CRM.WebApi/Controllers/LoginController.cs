using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.WebApi.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Queries;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using CRCIS.Web.INoor.CRM.Contract.ElkSearch;
using Microsoft.AspNetCore.Authentication;
using CRCIS.Web.INoor.CRM.WebApi.OpenId;

namespace CRCIS.Web.INoor.CRM.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserSearch _userSearch;
        private readonly IIdentityClient _identityClient;
        public LoginController(IAdminRepository adminRepository, IMapper mapper,
            IJwtProvider jwtProvider, ITokenStoreService tokenStoreService, ILoggerFactory loggerFactory,
            IAdminService adminService, IConfiguration configuration, IUserSearch userSearch, IIdentityClient identityClient)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _tokenStoreService = tokenStoreService;
            _logger = loggerFactory.CreateLogger<LoginController>(); ;
            _adminService = adminService;
            _configuration = configuration;
            _userSearch = userSearch;
            _identityClient = identityClient;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userClaimas = HttpContext.User.Claims.ToList();
            var inoorIdString = userClaimas.FirstOrDefault(a => a.Type == "sub").Value;
            var inoorId = Guid.Parse(inoorIdString);
            var admin = await _adminRepository.FindAdminAsync(inoorId);

            if (admin == null || admin.Success == false)
            {
                var result = new DataResponse<Guid>(admin.ApiErrors);
                return Ok(result);
            }

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userInfoResponse = await _identityClient.GetUserInfoAsync(accessToken);


            var name = userInfoResponse.Claims.Where(a => a.Type == "given_name").Select(a => a.Value).FirstOrDefault();
            var family = userInfoResponse.Claims.Where(a => a.Type == "family_name").Select(a => a.Value).FirstOrDefault();
            var username = userInfoResponse.Claims.Where(a => a.Type == "preferred_username").Select(a => a.Value).FirstOrDefault();
            var personId = inoorId;

            if (admin.Data == null)
            {
                //return new DataResponse<Guid>(new List<string> { "Admin not found" });
                var command = new Domain.Users.Admin.Commands.AdminCreateCommand(username, username + username, name, family, "", personId);
                await _adminRepository.CreateAsync(command);
            }
            else
            {
                var command = new Domain.Users.Admin.Commands.UpdateAdminInfoCommand
                {
                    Id = inoorId,
                    Name = name,
                    Family = family,
                    Username = username,
                };
                if (userInfoResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    await _adminRepository.UpdateAdminInfoAsync(command);

                }
            }

            admin = await _adminRepository.FindAdminAsync(personId);
            var dataResponse = await _adminService.GetVerifyTokenForNoorAdmin(username, name, family, personId, "Dashboard", null);
            return Redirect($"{_configuration["VueUrl"]}login?verifytoken={dataResponse.Data}");

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
                ValidToMilliseconds = (expireDate - DateTime.UtcNow).TotalMilliseconds,
                //User = responseUser.Data.Id == 1 ? "crmAdministrator" : "admin",//hard code permission
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
