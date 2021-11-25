using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Notifications
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(MailRequest mailRequest, MailSettings mailSettings);
    }
}
