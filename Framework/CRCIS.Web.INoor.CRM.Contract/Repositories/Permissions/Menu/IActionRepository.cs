using CRCIS.Web.INoor.CRM.Domain.Permissions.Action.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Menu
{
    public interface IActionRepository
    {
        Task<DataResponse<ActionDto>> GetAsync();
    }
}
