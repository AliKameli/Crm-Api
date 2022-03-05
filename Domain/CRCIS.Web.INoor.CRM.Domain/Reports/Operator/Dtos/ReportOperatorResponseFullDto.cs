using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Dtos
{
    public class ReportOperatorResponseFullDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid? NoorUserId { get; set; }
        public string ProductTitle { get; set; }
        public long RowNumber { get; set; }
        public string SourceTypeTitle { get; set; }
        public int TblNumber { get; set; }
        public string TblName { get; set; }
        public int? AdminId { get; set; }
        public string FirstSubject { get; set; }
        public string OperationTypeId { get; set; }
        public DateTime OperationTypeDateTime { get; set; }
        public string OperationTypeTitle { get; set; }
        public string AdminFullName { get; set; }
        public long TotalCount { get; set; }
        public long CaseId { get; set; }
        public int CaseHistoryId { get; set; }
        public string CreateDateTimePersian { get; set; }
        public string OperationTypeDateTimePersian { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string NameFamily { get; set; }
    }
}
