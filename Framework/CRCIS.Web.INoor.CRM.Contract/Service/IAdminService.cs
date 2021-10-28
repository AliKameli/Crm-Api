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
      
        Task<DataResponse<Guid>> GetVerifyTokenForNoorAdmin(string username, string name, string family, Guid personId, string action, Dictionary<string, string> queryString = null);
    }
}
