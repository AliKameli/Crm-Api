using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries
{
    public class ImportantWarningsDayQuery
    {
        public DateTime FromDay { get;private set; }
        public string ShowLogTypeIds { get;private set; }

        public ImportantWarningsDayQuery(DateTime fromDay, string showLogTypeIds)
        {
            FromDay = fromDay;
            ShowLogTypeIds = showLogTypeIds;
        }
    }
}
