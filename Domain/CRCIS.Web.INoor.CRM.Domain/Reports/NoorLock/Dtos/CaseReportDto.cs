using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos
{
    public class CaseReportDto
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string ReplyText { get; set; }
        public DateTime? ReplyDate { get; set; }
        public Guid? InoorId { get; set; }
        public long RowNumber { get; set; }
    }
}
