using CRCIS.Web.INoor.CRM.Domain.Permissions.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IPermissionService
    {
        Task<DataTableResponse<IEnumerable<PermissionDto>>> GetPermissionsByAdminIdAsync(int adminId);
    }
}
