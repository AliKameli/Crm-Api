using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Authentication
{
    public interface ITokenStoreService
    {
        Task<DataResponse<int>> CreateAsync(AccessTokenData accessToken, int adminId);
        Task<bool> IsValidTokenAsync(string accessToken, int adminId);
    }
}
