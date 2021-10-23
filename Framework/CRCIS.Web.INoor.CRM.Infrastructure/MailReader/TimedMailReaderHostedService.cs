using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Email.Commands;
using CRCIS.Web.INoor.CRM.Domain.Email.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using MailKit.Net.Pop3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
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
        public TimedMailReaderHostedService(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<TimedMailReaderHostedService>();
            _serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
              TimeSpan.FromMinutes(5));

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
                var dataResponse = _sourceConfigRepository.GetBySourceTypesId(2).Result;
                if (dataResponse.Success == false)
                {
                    _logger.LogCritical("GetBySourceTypeId faild {mails}");
                    return;
                }
                foreach (var item in dataResponse.Data)
                {
                    var mails = new List<EmailMessageDto>();
                    try
                    {
                        if (string.IsNullOrEmpty(item.ConfigJson))
                        {
                            _logger.LogCritical("(string.IsNullOrEmpty(item.ConfigJson)  {item.ConfigJson} ", item.ConfigJson);
                            break;
                        }
                        var configJsonDto = System.Text.Json.JsonSerializer.Deserialize<SourceConfigJsonDto>(item.ConfigJson);
                        if (configJsonDto == null)
                        {
                            _logger.LogCritical("(configJsonDto == null  {configJsonDto} ", configJsonDto);
                            break;
                        }
                        var mailProcessDateTimeNow = DateTime.Now;
                        mails.AddRange(
                            readMails(mailProcessDateTimeNow, configJsonDto.MailBox, configJsonDto.MailAddress, configJsonDto.MailPassword, item.LastUpdateTime)
                            );
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
               configJsonDto.MailAddress
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

            var ids = mailClient.GetMessageUids();
            for (int i = 0; i < mailClient.Count/* && i < maxCount*/; i++)
            {
                var uid = mailClient.GetMessageUid(i);
                var messageDateTime = uid.GetMailDate();
                if (messageDateTime < lastUpdateDate)
                {
                    continue;
                }
                var message = mailClient.GetMessage(i);
                dynamic from = message.From.FirstOrDefault();
                var name = from.Name;
                var address = from.Address;

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
                //emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddressDto { Address = x.Address, Name = x.Name }));
                //emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddressDto { Address = x.Address, Name = x.Name }));
                //emails.Add(emailMessage);

                result.Add(emailMessage);
            }

            return result;

        }

    }
}

