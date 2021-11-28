using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries
{
    public class NoorAppReportCaseIdQuery
    {
        public long CaseId { get;private set; }

        public NoorAppReportCaseIdQuery(long caseId)
        {
            CaseId = caseId;
        }
    }
}
