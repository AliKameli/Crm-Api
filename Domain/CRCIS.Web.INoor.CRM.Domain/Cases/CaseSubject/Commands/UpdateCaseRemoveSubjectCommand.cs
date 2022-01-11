using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands
{
    public class UpdateCaseRemoveSubjectCommand
    {
        public long CaseId { get; private set; }
        public int SubjectId { get; private set; }

        public UpdateCaseRemoveSubjectCommand(long caseId, int subjectId)
        {
            CaseId = caseId;
            SubjectId = subjectId;
        }
    }
}
