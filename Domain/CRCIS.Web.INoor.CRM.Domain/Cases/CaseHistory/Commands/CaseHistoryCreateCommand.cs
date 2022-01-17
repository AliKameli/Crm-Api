using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands
{
    public class CaseHistoryCreateCommand
    {
        public int? AdminId { get; private set; }
        public long CaseId { get; private set; }
        public DateTime OperationDateTime { get; private set; }
        public int OperationTypeId { get; private set; }


        public CaseHistoryCreateCommand(int? adminId, long caseId,
            DateTime operationDateTime, int operationTypeId)
        {
            AdminId = adminId;
            CaseId = caseId;
            OperationDateTime = operationDateTime;
            OperationTypeId = operationTypeId;
        }

    }
}
