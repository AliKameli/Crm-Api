using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Users.Token.Commands
{
    public class TokenCreateCommand
    {
        public string AccessTokenHash { get; private set; }
        public string RefreshTokenHash { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime ExpireAt { get; private set; }
        public int AdminId { get; private set; }

        public TokenCreateCommand( string accessTokenHash, string refreshTokenHash, DateTime expireAtUtc, int adminId)
        {
            AccessTokenHash = accessTokenHash;
            RefreshTokenHash = refreshTokenHash;
            ExpireAt = expireAtUtc;
            AdminId = adminId;
            CreateAt = DateTime.UtcNow;
        }
    }
}
