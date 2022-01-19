using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos
{
    public class PendingCaseGetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string ProductTitle { get; set; }
        public Guid? NoorUserId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime ImportDateTime { get; set; }
        public long RowNumber { get; set; }
        public string SourceTypeTitle { get; set; }
        public string FirstSubject { get; set; }
        public bool IsRead { get; set; }
        public long TotalCount { get; set; }
    }
}