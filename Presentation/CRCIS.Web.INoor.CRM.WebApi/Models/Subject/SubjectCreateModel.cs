using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Subject
{
    public class SubjectCreateModel
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public int? Priority { get; set; }
        public bool IsActive { get; set; }
    }
}
