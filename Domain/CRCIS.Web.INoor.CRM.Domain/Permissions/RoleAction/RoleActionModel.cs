using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction
{
    public class RoleActionModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ActionId { get; set; }
    }
}
