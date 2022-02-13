using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Commands
{
    public class RoleAdminUpdateCommand
    {
        public int AdminId { get; set; }
        public string RoleIds { get; set; }
        public RoleAdminUpdateCommand(int adminId, List<int> roleIds)
        {
            AdminId = adminId;
            RoleIds = roleIds == null ? "" : string.Join(',', roleIds);
        }
    }
}