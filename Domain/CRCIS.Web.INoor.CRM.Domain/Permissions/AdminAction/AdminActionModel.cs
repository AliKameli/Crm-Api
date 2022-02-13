using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.AdminAction
{
    public class AdminActionModel
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int ActionId { get; set; }
        public int? PromoterAdminId { get; set; }
    }
}