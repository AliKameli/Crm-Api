using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.Notifications
{
    public class NotificationValueDataEntered
    {
        public long CaseId { get; private set; }
        public int SourceConfigId { get;private set; }
        public long PendingHistoryId { get; private set; }
        public AnswerMethod AnswerMethod { get;private set; }

        public MailRequest MailRequest { get;private set; }
        public MailSettings MailSettings { get;private set; }
        public SmsRequest SmsRequest { get;private set; }

        public NotificationValueDataEntered(long caseId, int sourceConfigId, long pendingHistoryId,
            AnswerMethod answerMethod, MailRequest mailRequest, MailSettings mailSettings, SmsRequest smsRequest)
        {
            CaseId = caseId;
            SourceConfigId = sourceConfigId;
            PendingHistoryId = pendingHistoryId;
            AnswerMethod = answerMethod;
            MailRequest = mailRequest;
            MailSettings = mailSettings;
            SmsRequest = smsRequest;
        }
       
    }
}
