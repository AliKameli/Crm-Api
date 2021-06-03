using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Service
{
    public class AdminService : IAdminService
    {
        private readonly IIdentity _identity;
        private readonly ISecurityService _securityService;
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminVerifyTokenRepository _adminVerifyTokenRepository;
        public AdminService(IIdentity identity, ISecurityService securityService,
            IAdminRepository adminRepository,
            IAdminVerifyTokenRepository adminVerifyTokenRepository)
        {
            _identity = identity;
            _securityService = securityService;
            _adminRepository = adminRepository;
            _adminVerifyTokenRepository = adminVerifyTokenRepository;
        }

        public async Task<DataResponse<int>> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            var adminId = _identity.GetAdminId();
            var admin = await _adminRepository.FindAdminAsync(adminId);
            if (admin.Success == false || admin?.Data == null)
                return new DataResponse<int>(new string[] { "خطایی رخ داده است" });

            var eq = _securityService.IsEquals(oldPassword, admin.Data.PasswordHash);
            if (eq == false)
                return new DataResponse<int>(new string[] { "پسورد صحیح نیست" });

            var newPasswordHash = _securityService.GetSha256Hash(newPassword);
            var response = await _adminRepository.UpdatePasswordHashAsync(adminId, newPasswordHash);
            return response;
        }

        public async Task<DataResponse<Guid>> GetVerifyTokenForNoorAdmin(string username, string name, string family, string personId, string action, Dictionary<string, string> queryString = null)
        {
            var admin = await _adminRepository.FindAdminAsync(personId);
            if (admin == null || admin.Success == false)
            {
                var command = new Domain.Users.Admin.Commands.AdminCreateCommand(username, username + username, name, family, "", personId);
                await _adminRepository.CreateAsync(command);
                admin = await _adminRepository.FindAdminAsync(personId);
            }
            if (admin.Success == false)
            {
                return new DataResponse<Guid>(admin.ApiErrors);
            }

            var token = await _adminVerifyTokenRepository.CreateTokenAsync(admin.Data.Id, queryString, action);

            return token;
        }
    }
}
