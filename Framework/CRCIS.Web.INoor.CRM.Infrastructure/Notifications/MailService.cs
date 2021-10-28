using CRCIS.Web.INoor.CRM.Contract.Notifications;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Notifications
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(MailRequest mailRequest, MailSettings mailSettings)
        {
            var oMail =
                new MailMessage(new MailAddress(mailSettings.Mail),
                new MailAddress(mailRequest.ToEmail))
                {
                    Subject = mailRequest.Subject,
                    SubjectEncoding = new UTF8Encoding(),
                    Body = mailRequest.Body,
                    BodyEncoding = new UTF8Encoding(),
                    IsBodyHtml = true
                };

            using var smtpClient = new SmtpClient
            {
                Host = mailSettings.Host,
                Credentials = new NetworkCredential(mailSettings.Mail, mailSettings.Password),
                //EnableSsl = false,
                Port = mailSettings.Port,// 25
            };

            try
            {
                smtpClient.Send(oMail);
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                // ignoared
            }
            oMail.Dispose();

        }
    }
}
