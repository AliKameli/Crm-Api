using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.RoleAdmin
{
    public interface IRoleAdminRepository
    {
        Task<DataTableResponse<IEnumerable<int>>> GetPermissionActionListByAdminId(int adminId);
        Task<DataTableResponse<IEnumerable<RoleAdminGetDto>>> GetRolesInAdmin(int adminId);
        Task<DataResponse<int>> UpdatAdminRolesAsync(RoleAdminUpdateCommand command);
    }
}
