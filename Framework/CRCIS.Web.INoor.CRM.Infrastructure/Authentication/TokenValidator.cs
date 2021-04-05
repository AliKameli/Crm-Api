using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Authentication
{
    public class TokenValidator: ITokenValidator
    {
        private readonly ITokenStoreService _tokenStoreService;
        private readonly IAdminRepository _adminRepository;
        public TokenValidator(IAdminRepository adminRepository, ITokenStoreService tokenStoreService)
        {
            _adminRepository = adminRepository;
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

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (!int.TryParse(userIdString, out int adminId))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            var dataResponse = await _adminRepository.GetByIdAsync(adminId);
            if (dataResponse.Success == false)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }
            var admin = dataResponse?.Data;
            if (admin == null || admin.SerialNumber != serialNumberClaim.Value || !admin.IsActive)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This token is expired. Please login again.");
                return;
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
