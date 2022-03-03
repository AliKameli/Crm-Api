using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Subject.Dtos
{
    public class ReportSubjectDto
    {

        public long CaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? NoorUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string ProductTitle { get; set; }
        public long RowNumber { get; set; }
        public string SourceTypeTitle { get; set; }
        public int TblNumber { get; set; }
        public string TblName { get; set; }
        public int? AdminId { get; set; }
        public long TotalCount { get; set; }

        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public DateTime SubjectCaseCreateAt { get; set; }
    }
}
