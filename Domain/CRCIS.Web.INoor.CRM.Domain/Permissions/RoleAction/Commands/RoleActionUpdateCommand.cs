using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction.Commands
{
    public class RoleActionUpdateCommand
    {
        public int RoleId { get; set; }
        public string ActionIds { get; set; }
        public RoleActionUpdateCommand(int roleId, List<int> actionIds)
        {
            RoleId = roleId;
            ActionIds = actionIds == null ? "" : string.Join(',', actionIds);
        }
    }
}
