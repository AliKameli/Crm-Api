using CRCIS.Web.INoor.CRM.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.Menu
{
    public class ActionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PersianName { get; set; }
        public int? ParentId { get; set; }
        public ActionType ActionType { get; set; }
    }
}
