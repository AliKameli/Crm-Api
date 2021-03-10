using CRCIS.Web.INoor.CRM.Contract.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using System.IdentityModel.Tokens.Jwt;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Authentication
{
    public class JwtValidator : IJwtValidator
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenStoreService _tokenStoreService;

        public JwtValidator(IAdminRepository userRepository, ITokenStoreService tokenStoreService)
        {
            _adminRepository = userRepository;
            _tokenStoreService = tokenStoreService;
        }
        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                context.Fail("This is not our issued token. It has no serial.");
                return;
            }

            var adminIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (!int.TryParse(adminIdString, out int adminId))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            var user = await _adminRepository.FindAdminAsync(adminId);
            if (user == null || user.SerialNumber != serialNumberClaim.Value || !user.IsActive)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This token is expired. Please login again.");
            }

            if (!(context.SecurityToken is JwtSecurityToken accessToken) || string.IsNullOrWhiteSpace(accessToken.RawData) ||
                !await _tokenStoreService.IsValidTokenAsync(accessToken.RawData, adminId))
            {
                context.Fail("This token is not in our database.");
                return;
            }

            await _adminRepository.UpdateAdminLastActivityDateAsync(adminId);
        }
    }
}
