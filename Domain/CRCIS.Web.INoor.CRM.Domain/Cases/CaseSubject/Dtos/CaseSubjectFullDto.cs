using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Dtos
{
    public class CaseSubjectFullDto
    {
        public long Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int SubjectCode { get; set; }
        public long CaseId { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsPrimary { get; set; }

    }
}
