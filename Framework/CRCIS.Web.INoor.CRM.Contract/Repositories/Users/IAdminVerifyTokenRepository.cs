using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Users
{
    public interface IAdminVerifyTokenRepository
    {
        Task<DataResponse<Guid>> CreateTokenAsync(int adminId);
        Task<DataResponse<AdminModel>> GetAdminByVerifyToken(Guid verifyId);
    }
}
