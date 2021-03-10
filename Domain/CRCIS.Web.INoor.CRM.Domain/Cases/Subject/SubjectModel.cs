using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public int? Priority { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; }
    }
}
