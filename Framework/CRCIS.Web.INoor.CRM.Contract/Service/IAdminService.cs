using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IAdminService
    {
        Task<DataResponse<int>> ChangePasswordAsync(string oldPassword, string newPassword);
    }
}
