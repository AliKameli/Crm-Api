using CRCIS.Web.INoor.CRM.Contract.Notifications;
using MailKit.Security;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Notifications
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(MailRequest mailRequest, MailSettings mailSettings)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            if (string.IsNullOrEmpty(mailSettings.Password) == false)
            {
                smtp.Authenticate(mailSettings.UserName, mailSettings.Password);
            }
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
