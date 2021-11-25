using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos
{
    public class CaseHistoriesDto
    {
        public long CaseId { get; set; }
        public long?PendingHistoryId { get; set; }
        public string AnswerMethodTitle { get; set; }
        public string AnswerText { get; set; }
        public bool IsAnswering { get; set; }
        public bool OnlySaving { get; set; }
        public bool AnswerResult { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string OperationDatePersian { get; set; }
        public string OperationTimePersian { get; set; }
        public string OperationTypeTitle { get; set; }
        public string AdminFullName { get; set; }
    }
}
