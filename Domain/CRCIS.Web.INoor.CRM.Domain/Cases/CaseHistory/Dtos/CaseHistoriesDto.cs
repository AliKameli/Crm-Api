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
        public long PendingHistoryId { get; set; }
        public string AnswerMethodTitle { get; set; }
        public string AnswerText { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string OperationDateTimePersian { get; set; }
        public string OperationTypeTitle { get; set; }
    }
}
