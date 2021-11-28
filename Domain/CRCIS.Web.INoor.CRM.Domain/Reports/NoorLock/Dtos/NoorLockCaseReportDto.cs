using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos
{
    public class NoorLockCaseReportDto
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string AnswerText { get; set; }
        public DateTime? AnswerDate { get; set; }
        public Guid? InoorId { get; set; }
        public long RowNumber { get; set; }
        public long TotalCount { get; set; }
    }
}