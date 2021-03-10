using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Domain.Users.Token.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Authentication
{
    public class TokenStoreService : ITokenStoreService
    {

        private readonly ISecurityService _securityService;
        private readonly ITokenRepository _tokenRepository;
        public TokenStoreService(ISecurityService securityService, ITokenRepository tokenRepository)
        {
            _securityService = securityService;
            _tokenRepository = tokenRepository;
        }

        public Task<bool> IsValidTokenAsync(string accessToken, int adminId)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<int>> CreateAsync(AccessTokenData accessToken, int adminId)
        {
            var hashRefresh = _securityService.GetSha256Hash(accessToken.RedreshToken);
            var hashAccess = _securityService.GetSha256Hash(accessToken.AccessToken);

            var command = new TokenCreateCommand(hashAccess, hashRefresh, accessToken.ExpireAtUtc, adminId);

            return await _tokenRepository.CreateAsync(command);

        }
    }
}
