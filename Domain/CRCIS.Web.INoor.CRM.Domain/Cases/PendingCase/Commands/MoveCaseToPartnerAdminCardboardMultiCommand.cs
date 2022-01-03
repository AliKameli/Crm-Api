using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands
{
    public class MoveCaseToPartnerAdminCardboardMultiCommand
    {
        public int AdminId { get; set; }
        public List<long> CaseIds { get; private set; }

        public MoveCaseToPartnerAdminCardboardMultiCommand(List<long> caseIds, int adminId)
        {
            CaseIds = caseIds;
            AdminId = adminId;
        }
    }
}
