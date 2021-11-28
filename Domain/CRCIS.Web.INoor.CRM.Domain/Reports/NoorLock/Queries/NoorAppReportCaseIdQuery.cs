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
        public Guid NoorUserId { get; private set; }
        public string ProductSecret { get; private set; }

        public NoorAppReportCaseIdQuery(long caseId, Guid noorUserId, string productSecret)
        {
            CaseId = caseId;
            NoorUserId = noorUserId;
            ProductSecret = productSecret;
        }
    }
}
