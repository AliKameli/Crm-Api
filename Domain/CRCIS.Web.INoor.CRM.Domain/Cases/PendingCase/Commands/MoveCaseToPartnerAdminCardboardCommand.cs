using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands
{
  public  class MoveCaseToPartnerAdminCardboardCommand
    {
        public long AdminId { get; private set; }
        public long CaseId { get; private set; }

        public MoveCaseToPartnerAdminCardboardCommand(long adminId, long caseId)
        {
            AdminId = adminId;
            CaseId = caseId;
        }
    }
}
