using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands
{
    public class MoveCaseToCurrentAdminCardboardCommand
    {
        public int AdminId { get; private set; }
        public long CaseId { get; private set; }

        public MoveCaseToCurrentAdminCardboardCommand(int adminId, long caseId)
        {
            AdminId = adminId;
            CaseId = caseId;
        }
    }
}
