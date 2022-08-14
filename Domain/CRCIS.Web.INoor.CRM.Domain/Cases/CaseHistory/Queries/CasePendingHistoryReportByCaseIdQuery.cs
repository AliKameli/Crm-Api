using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Queries
{
    public class CasePendingHistoryReportByCaseIdQuery
    {
        public long CaseId { get;private set; }
        public string AnswerMethodIds { get;private set; }

        public CasePendingHistoryReportByCaseIdQuery(long id, string answerMethodIds)
        {
            CaseId = id;
            AnswerMethodIds = answerMethodIds;
        }
    }
}
