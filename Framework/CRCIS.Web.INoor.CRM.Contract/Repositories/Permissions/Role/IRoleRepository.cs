using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Queris;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role
{
    public interface IRoleRepository
    {
        Task<DataResponse<int>> CreateAsync(RoleCreateCommand command);
        Task<DataTableResponse<IEnumerable<RoleGetDto>>> GetAsync(RoleDataTableQuery query);
        Task<DataTableResponse<IEnumerable<RoleGetShowTreeDto>>> GetShowTreeAsync();
        Task<DataResponse<int>> UpdateAsync(RoleUpdateCommand command);
    }
}
