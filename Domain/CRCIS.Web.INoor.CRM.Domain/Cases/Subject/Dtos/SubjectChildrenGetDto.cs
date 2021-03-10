using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Dtos
{
    public class SubjectChildrenGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; }
        public int TotalCount { get; set; }
        public int? Priority { get; set; }
        public int RowNumber { get; set; }
        public int SubTreeCount { get; set; }
    }
}
