using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.Notifications
{
    public class NotificationConsumer : IConsumer<NotificationValueDataEntered>
    {
        private readonly IPendingHistoryRepository _pendingHistoryRepository;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;

        public NotificationConsumer(IPendingHistoryRepository pendingHistoryRepository, IMailService mailService, ISmsService smsService)
        {
            _pendingHistoryRepository = pendingHistoryRepository;
            _mailService = mailService;
            _smsService = smsService;
        }

        public async Task Consume(ConsumeContext<NotificationValueDataEntered> context)
        {
            if (context.Message.AnswerMethod == Utility.Enums.AnswerMethod.Sms)
            {
                var result = await _smsService.SendSmsAsync(context.Message.SmsRequest);
                await this.UpdateResultAsync(context.Message.PendingHistoryId, result, context.Message.SourceConfigId, context.Message.SmsRequest.ToMobile);
            }
            if (context.Message.AnswerMethod == Utility.Enums.AnswerMethod.Email)
            {
                var result = _mailService.SendEmail(context.Message.MailRequest, context.Message.MailSettings);
                await this.UpdateResultAsync(context.Message.PendingHistoryId, result, context.Message.SourceConfigId, context.Message.MailRequest.ToEmail);
            }
        }

        private Task UpdateResultAsync(long pendingHistoryId, bool result, int sourceConfigId, string answerTarget)
        {
            var command =
            new AnsweringUpdateResultCommand
            {
                PendingHistoryId = pendingHistoryId,
                SourceConfigId = sourceConfigId,
                Result = result,
                AnswerTarget = answerTarget
            };
            return _pendingHistoryRepository.UpdateResulAsync(command);
        }

    }
}
