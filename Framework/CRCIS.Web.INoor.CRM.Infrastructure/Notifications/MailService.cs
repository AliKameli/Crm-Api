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

            var oMail = new MailMessage(new MailAddress("no-reply@noornet.ir"), new MailAddress(mailRequest.ToEmail))
            {
                Subject = mailRequest.Subject,
                SubjectEncoding = new UTF8Encoding(),
                Body = mailRequest.ToEmail,
                BodyEncoding = new UTF8Encoding(),
                IsBodyHtml = true
            };

            //if (Attachments != null)
            //{
            //    foreach (var item in Attachments)
            //        oMail.Attachments.Add(new System.Net.Mail.Attachment(item));
            //}


            var oSmtp = new SmtpClient
            {
                //Host = "172.16.25.16",//"mail.noornet.net",
                Host = mailSettings.Host,
                //Credentials = new NetworkCredential("andr3yy.design", "password"),
                Credentials = new NetworkCredential(mailSettings.Mail, mailSettings.Password),
                //EnableSsl = false,
                //Port = 25
            };

            try
            {
                oSmtp.Send(oMail);
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                // ignoared
            }
            oMail.Dispose();

            // Plug in your email service here to send an email.
            //return Task.;


            //var email = new MimeMessage();
            //email.From .Add(new MailboxAddress(mailSettings.Mail));
            //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            //email.Subject = mailRequest.Subject;
            //var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            //builder.HtmlBody = mailRequest.Body;
            //email.Body = builder.ToMessageBody();

            //using var smtp = new SmtpClient();
            //smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            //if (string.IsNullOrEmpty(mailSettings.Password) == false)
            //{
            //    smtp.Authenticate(mailSettings.UserName, mailSettings.Password);
            //}
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);
        }
    }
}
