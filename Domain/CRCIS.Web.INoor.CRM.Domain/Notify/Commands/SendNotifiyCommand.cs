using CRCIS.Web.INoor.CRM.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Notify.Commands
{
    public class SendNotifiyCommand
    {
        public int AnswerMethodId { get;private set; }
        public long  CaseId { get;private set; }
        public string AnswerSource { get;private set; }
        public string AnswerText { get;private set; }
        public long PendingHistoryId { get;private set; }

        public SendNotifiyCommand(int answerMethodId, long caseId, string answerSource, string answerText, long pendingHistoryId)
        {
            AnswerMethodId = answerMethodId;
            CaseId = caseId;
            AnswerSource= answerSource;
            AnswerText = answerText;
            PendingHistoryId = pendingHistoryId;
        }

    }
}
