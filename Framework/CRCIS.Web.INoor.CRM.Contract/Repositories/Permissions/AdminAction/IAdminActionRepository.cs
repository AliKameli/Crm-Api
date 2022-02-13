using CRCIS.Web.INoor.CRM.Domain.Permissions.AdminAction.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.AdminAction
{
    public interface IAdminActionRepository
    {
        Task<DataTableResponse<IEnumerable<AdminActionDto>>> GetAdminActionByAdminIdAsync(int adminId);
    }
}
