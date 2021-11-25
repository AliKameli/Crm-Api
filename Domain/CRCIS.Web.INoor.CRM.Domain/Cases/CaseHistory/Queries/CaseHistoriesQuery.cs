using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Queries
{
    public class CaseHistoriesQuery
    {
        public long CaseId { get; set; }
        public long? PendingHistoryId { get; set; }
        public int? AnswerMethodId { get; set; }
        public string AnswerMethodTitle { get; set; }
        public string AnswerText { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string OperationTypeTitle { get; set; }
        public string AdminName { get; set; }
        public string AdminFamily { get; set; }
        public bool AnswerResult { get; set; }
    }
}
