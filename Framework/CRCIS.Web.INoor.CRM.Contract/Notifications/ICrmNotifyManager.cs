using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Notifications
{
    public interface ICrmNotifyManager
    {
        Task<DataResponse<string>> SendEmailAsync(long caseId, string fromMailBox, string message);
        Task<DataResponse<string>> SendSmsAsync(long caseId, string fromSmsCenter, string message);
    }
}
