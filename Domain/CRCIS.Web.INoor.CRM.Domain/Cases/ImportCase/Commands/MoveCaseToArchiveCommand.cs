using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands
{
    public class MoveCaseToArchiveCommand
    {
        public long CaseId { get; private set; }

        public MoveCaseToArchiveCommand(long caseId)
        {
            CaseId = caseId;
        }
    }
}