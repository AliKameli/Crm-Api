using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands
{
    public class MoveCaseToCurrentAdminCardboardMultiCommand
    {
        public int AdminId { get; private set; }
        public List<long> CaseIds { get; private set; }
        public MoveCaseToCurrentAdminCardboardMultiCommand(int adminId, List<long> caseIds)
        {
            AdminId = adminId;
            CaseIds = caseIds;
        }
    }
}
