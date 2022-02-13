using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role
{
    public interface IRoleActionRepository
    {
        Task<DataTableResponse<IEnumerable<RoleActionGetDto>>> GetActionsInRoleAsync(int roleId);
        Task<DataResponse<int>> UpdateRoleActionAsync(RoleActionUpdateCommand command);
    }
}
