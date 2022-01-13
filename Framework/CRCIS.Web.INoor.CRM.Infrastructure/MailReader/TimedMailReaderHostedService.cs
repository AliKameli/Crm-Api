using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Email.Commands;
using CRCIS.Web.INoor.CRM.Domain.Email.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using MailKit.Net.Pop3;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.MailReader
{
    public class TimedMailReaderHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private int executionCount = 0;
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        public TimedMailReaderHostedService(ILoggerFactory loggerFactory, IServiceProvider serviceProvider, IWebHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<TimedMailReaderHostedService>();
            _serviceProvider = serviceProvider;
            _hostEnvironment = hostEnvironment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
              TimeSpan.FromMinutes(2));

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            return Task.CompletedTask;

        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);
            var time = DateTime.Now;
            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count} {time}", count, time);
            mailBoxInserProcess();

        }

        private void mailBoxInserProcess()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ISourceConfigRepository _sourceConfigRepository = scope.ServiceProvider.GetRequiredService<ISourceConfigRepository>();
                var dataResponse = _sourceConfigRepository.GetBySourceTypesIdAsync(2).Result;
                if (dataResponse.Success == false)
                {
                    _logger.LogCritical("GetBySourceTypeId faild reading mails");
                    return;
                }
                var resoures = dataResponse.Data.ToList();
                foreach (var item in resoures)
                {
                    var mails = new List<EmailMessageDto>();
                    try
                    {
                        if (string.IsNullOrEmpty(item.ConfigJson))
                        {
                            _logger.LogCritical("(string.IsNullOrEmpty(item.ConfigJson)  {item.ConfigJson} ", item.ConfigJson);
                            continue;
                        }
                        _logger.LogInformation(item.ConfigJson);
                        var configJsonDto = System.Text.Json.JsonSerializer.Deserialize<SourceConfigJsonDto>(item.ConfigJson);
                        if (configJsonDto == null)
                        {
                            _logger.LogCritical("(configJsonDto == null  {configJsonDto} ", configJsonDto);
                            continue;
                        }
                        var mailProcessDateTimeNow = DateTime.Now;
                        mails =
                            readMails(mailProcessDateTimeNow, configJsonDto.MailBox, configJsonDto.MailAddress, configJsonDto.MailPassword, item.LastUpdateTime)
                            .ToList();
                        insertMails(mailProcessDateTimeNow, item, configJsonDto, mails);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical("Exception {ex}", ex.Message);
                    }
                }
            }
        }

        private void insertMails(DateTime processDateTimeNow, SourceConfigModel sourceConfig, SourceConfigJsonDto configJsonDto, List<EmailMessageDto> mails)
        {
            var insertStatus = false;

            var mailboxImportCommands = mails.Select(mail =>
               new MailboxImportCommand(mail.Subject, mail.FromName, mail.FromEmail, mail.Content,
               2,
               sourceConfig.ProductId,
               mail.CreateDate,
               configJsonDto.MailAddress,
               mail.AttachemntFiles
                 ));

            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IRabbitImportCaseRepository _importCaseRepository = scope.ServiceProvider.GetRequiredService<IRabbitImportCaseRepository>();
                    var insertResponse = _importCaseRepository
                        .CreateFromMailboxImportAsync(
                        mailboxImportCommands,
                        sourceConfig.Id,
                        processDateTimeNow
                        ).Result;
                    insertStatus = insertResponse.Success;
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception {ex}", ex.Message);
            }
        }


        private IEnumerable<EmailMessageDto> readMails(DateTime now, string mailBox, string mailAddress, string mailPassword, DateTime lastUpdateDate)
        {

            var result = new List<EmailMessageDto>();
            using var mailClient = new Pop3Client();
            mailClient.Connect(mailBox, 110, MailKit.Security.SecureSocketOptions.None);
            mailClient.AuthenticationMechanisms.Remove("XAUTH2");
            mailClient.Authenticate(mailAddress, mailPassword);

            _logger.LogInformation($"{mailAddress} >total count: {mailClient.Count} > now value: {now}");

            var ids = mailClient.GetMessageUids();
            _logger.LogInformation($"{mailAddress} > start {DateTime.Now}");
            _logger.LogInformation($"{mailAddress} > end {DateTime.Now}");

            var index = mailClient.Count == 0 ? 0 : mailClient.Count - 1;
            for (int i = index; i >= 0/* && i < maxCount*/; i++)
            {

                var uid = mailClient.GetMessageUid(i);
                var messageDateTime = uid.GetMailDate();
                if (messageDateTime < lastUpdateDate)
                {
                    break;
                }
                var message = mailClient.GetMessage(i);
                dynamic from = message.From.FirstOrDefault();

                string name = null, address = null;

                try { name = from.Name as string; }
                catch (Exception ex) { string s = ex.Message; };
                try { address = from.Address as string; }
                catch (Exception ex) { string s = ex.Message; }

                var emailMessage = new EmailMessageDto
                {
                    //Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                    Content = message.TextBody,
                    Subject = message.Subject,
                    FromName = name,
                    FromEmail = address,
                    CreateDate = messageDateTime,
                    ToMailBox = mailAddress,

                };

                try
                {
                    if (message.Attachments is not null && message.Attachments.Any())
                    {

                        emailMessage.AttachemntFiles = new List<EmailAttachmentDto>();
                        foreach (var attachment in message.Attachments)
                        {
                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                            var dto = new EmailAttachmentDto
                            {
                                Name = fileName,
                                Address = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}"
                            };

                            emailMessage.AttachemntFiles.Add(dto);
                            fileName = dto.Address;

                            Directory.CreateDirectory(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "mails"));

                            using (var stream = File.Create(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "mails", fileName)))
                            {
                                if (attachment is MessagePart)
                                {
                                    var rfc822 = (MessagePart)attachment;

                                    rfc822.Message.WriteTo(stream);
                                }
                                else
                                {
                                    var part = (MimePart)attachment;

                                    part.Content.DecodeTo(stream);
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                }

                result.Add(emailMessage);
            }
            _logger.LogInformation($"{mailAddress} > end {DateTime.Now}");

            result = result.OrderBy(a => a.CreateDate).ToList();
            return result;
        }

    }

}

