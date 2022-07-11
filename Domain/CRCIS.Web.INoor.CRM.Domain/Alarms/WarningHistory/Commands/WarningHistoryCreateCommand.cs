using CRCIS.Web.INoor.CRM.Utility.Enums;
using CRCIS.Web.INoor.CRM.Utility.Enums.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.WarningHistory.Commands
{
    public class WarningHistoryCreateCommand
    {
        public long WarningId { get;private set; }
        public DateTime CreateDate { get;private set; }
        public int? AdminId { get;private set; }
        public int WarningHistoryTypeId { get;private set; }

        public WarningHistoryCreateCommand(long warningId, int? adminId, WarningHistoryType warningHistoryType )
        {
            WarningId = warningId;
            AdminId = adminId;
            WarningHistoryTypeId = warningHistoryType.ToInt32(); ;
            CreateDate = DateTime.Now;
        }
    }
}
