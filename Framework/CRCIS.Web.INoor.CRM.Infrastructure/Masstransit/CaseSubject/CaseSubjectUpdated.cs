using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.CaseSubject
{
    public class CaseSubjectUpdated
    {
        public int SubjectId { get; set; }

        public CaseSubjectUpdated(int subjectId)
        {
            SubjectId = subjectId;
        }
    }
}
