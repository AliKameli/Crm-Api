using CRCIS.Web.INoor.CRM.Contract.Notifications;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Notifications
{
    public class MailService : IMailService
    {
        private readonly ILogger _logger;
        public MailService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MailService>();
        }
        public async Task<bool> SendEmailAsync(MailRequest mailRequest, MailSettings mailSettings)
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

            bool result = false;
            try
            {
                smtpClient.Send(oMail);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                var s = ex.Message;
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                _logger.LogError(ex.InnerException?.Message);
                _logger.LogError(ex.InnerException?.StackTrace);
                // ignoared

            }
            oMail.Dispose();
            return result;

        }
    }
}
