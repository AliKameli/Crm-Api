using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Notifications
{
    public class MailService : IMailService
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MailService(ILoggerFactory loggerFactory, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<MailService>();
            _hostEnvironment = hostEnvironment;
        }
        public async Task<bool> SendEmailAsync(MailRequest mailRequest, MailSettings mailSettings)
        {
            bool result = false;
            try
            {
                var oMail =
                   new MailMessage(new MailAddress(mailSettings.Mail),
                   new MailAddress(mailRequest.ToEmail))
                   {
                       Subject = mailRequest.Subject,
                       SubjectEncoding = new UTF8Encoding(),
                       Body = mailRequest.Body,
                       BodyEncoding = new UTF8Encoding(),
                       IsBodyHtml = true,
                   };
                if (mailRequest.Attachments is not null)
                {
                    foreach (var item in mailRequest.Attachments)
                    {
                        var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "uploads", item.Address);
                        FileStream fsSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        byte[] bytes = new byte[fsSource.Length];

                        var attachment = new Attachment(fsSource, item.Name);

                        oMail.Attachments.Add(attachment);
                    }
                }

                var smtpClient = new SmtpClient
                {
                    Host = mailSettings.Host,
                    Credentials = new NetworkCredential(mailSettings.Mail, mailSettings.Password),
                    //EnableSsl = false,
                    Port = mailSettings.Port,// 25
                };


                smtpClient.Send(oMail);
                result = true;
                if (mailRequest.Attachments is not null)
                    for (int i = 0; i < mailRequest.Attachments.Count; i++)
                    {
                        try
                        {
                            oMail.Attachments[i].Dispose();
                        }
                        catch (Exception)
                        {
                        }
                    }


                oMail.Dispose();
            }
            catch (Exception ex)
            {
                result = false;
                _logger.LogException(ex);
            }

            return result;

        }
    }
}
