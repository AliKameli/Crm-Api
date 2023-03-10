using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands
{
    public class MoveCaseToArchiveMultiCommand
    {
        public int AdminId { get; private set; }
        public List<long> CaseIds { get; private set; }

        public MoveCaseToArchiveMultiCommand(int adminId, List<long> caseIds)
        {
            AdminId = adminId;
            CaseIds = caseIds;
        }
    }
}
