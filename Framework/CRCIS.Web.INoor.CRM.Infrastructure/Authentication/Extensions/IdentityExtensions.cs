using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetAdminId(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var adminIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (string.IsNullOrEmpty(adminIdString))
            {
                throw new ArgumentNullException(nameof(adminIdString));
            }
            int adminId;
            if (int.TryParse(adminIdString, out adminId))
            {
                return adminId;
            }
            throw new ArgumentException(nameof(adminId));
        }
    }
}
