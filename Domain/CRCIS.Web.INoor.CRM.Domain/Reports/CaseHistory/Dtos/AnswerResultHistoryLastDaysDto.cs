using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos
{
    public class AnswerResultHistoryLastDaysDto
    {
        public int Day { get; set; }
        public int CNT { get; set; }
        public int PendingResultId { get; set; }
        public string PendingResultTitle { get; set; }
        public string PendingResultMessage { get; set; }
    }
}
