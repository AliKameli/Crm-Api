using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries
{
    public class NoorLockReportCaseIdQuery
    {
        public long CaseId { get;private set; }

        public NoorLockReportCaseIdQuery(long caseId)
        {
            CaseId = caseId;
        }
    }
}
