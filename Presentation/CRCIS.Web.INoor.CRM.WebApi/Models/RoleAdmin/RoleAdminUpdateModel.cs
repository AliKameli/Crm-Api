using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.RoleAdmin
{
    public class RoleAdminUpdateModel
    {
        public int AdminId { get; set; }
        public List<int> RoleIds { get; set; }

    }
}
