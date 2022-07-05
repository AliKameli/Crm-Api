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
        private readonly IPermissionService _permissionService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public LoginController(IAdminRepository adminRepository, IMapper mapper,
            IJwtProvider jwtProvider, ITokenStoreService tokenStoreService,
            IPermissionService permissionService, ILoggerFactory loggerFactory,
            IAdminService adminService, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _tokenStoreService = tokenStoreService;
            _permissionService = permissionService;
            _logger = loggerFactory.CreateLogger<LoginController>(); ;
            _adminService = adminService;
            _configuration = configuration;
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

            var username = "";
            var name = "";
            var family = "";
            var personId = inoorId;

            if (admin.Data == null)
            {
                //return new DataResponse<Guid>(new List<string> { "Admin not found" });
                var command = new Domain.Users.Admin.Commands.AdminCreateCommand(username, username + username, name, family, "", personId);
                await _adminRepository.CreateAsync(command);
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
