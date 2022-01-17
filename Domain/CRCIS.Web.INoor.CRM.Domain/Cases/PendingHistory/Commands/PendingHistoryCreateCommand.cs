using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingHistory.Commands
{
    public class PendingHistoryCreateCommand
    {
        public long CaseHistoryId { get; private set; }
        public int AnswerMethodId { get; private set; }
        public string AnswerText { get; private set; }
        public string AnswerSource { get;private set; }
        public string AtachmentsJsonData { get; private set; }

        public PendingHistoryCreateCommand(long caseHistoryId, int answerMethodId, string answerText, string answerSource, string atachmentsJsonData)
        {
            CaseHistoryId = caseHistoryId;
            AnswerMethodId = answerMethodId;
            AnswerText = answerText;
            AnswerSource = answerSource;
            AtachmentsJsonData = atachmentsJsonData;
        }
    }
}
