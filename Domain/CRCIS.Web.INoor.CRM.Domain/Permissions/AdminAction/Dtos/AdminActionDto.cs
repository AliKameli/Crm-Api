using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.AdminAction.Dtos
{
    public class AdminActionDto
    {
        public int AdminId { get; set; }
        public int ActionId { get; set; }
        public int TotalCount { get; set; }
    }
}
