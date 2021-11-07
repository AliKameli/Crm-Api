using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Enums.Extensions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Notifications
{
    public class CrmNotifyProvider : ICrmNotifyManager
    {
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IPendingCaseRepository _pendingCaseRepository;
        private readonly ISourceConfigRepository _sourceConfigRepository;
        private readonly ILogger _logger;

        public CrmNotifyProvider(ILoggerFactory loggerFactory,
            IMailService mailService,
            IPendingCaseRepository pendingCaseRepository,
            ISourceConfigRepository sourceConfigRepository,
            ISmsService smsService)
        {
            _mailService = mailService;
            _pendingCaseRepository = pendingCaseRepository;
            _sourceConfigRepository = sourceConfigRepository;
            _logger = loggerFactory.CreateLogger<CrmNotifyProvider>();
            _smsService = smsService;
        }

        public async Task<DataResponse<string>> SendEmailAsync(long caseId, string fromMailBox, string message)
        {
            var responsePendingCase = await _pendingCaseRepository.GetByIdAsync(caseId);
            if (responsePendingCase.Success == false || responsePendingCase.Data == null)
                return new DataResponse<string>(new List<string> { "خطا در واکشی اطلاعات" });


            if(string.IsNullOrEmpty(responsePendingCase?.Data?.Email))
                return new DataResponse<string>(new List<string> { "ایمیل مخاطب یافت نشد" });

            var dataResponseMails = _sourceConfigRepository.GetByAnswerMethodIdAsync((int)AnswerMethod.Email).Result;
            if (dataResponseMails.Success == false || dataResponseMails.Data == null)
            {
                _logger.LogCritical("GetBySourceTypeId faild reading mail settings {fromMailBox}", fromMailBox);
                return new DataResponse<string>(new List<string> { "تنظیمات ایمیل باکس یافت نشد" });
            }

            var mailSettings = dataResponseMails.Data.Select(m => funcSourceConfigJson(m.ConfigJson)).ToList();
            var mailSettingSelected = mailSettings.FirstOrDefault(config => config.MailAddress == fromMailBox);
            if (mailSettingSelected == null)
                return new DataResponse<string>(new List<string> { "تنظیمات ایمیل باکس یافت نشد" });
            if (mailSettingSelected.AllowSend == false)
                return new DataResponse<string>(new List<string> { " ایمیل باکس دسترسی ارسال ندارد" });

            var request = new MailRequest()
            {
                ToEmail = responsePendingCase?.Data?.Email,
                Subject = "پاسخگویی مرکز نور",
                Body = message
            };
            var mailSetting = new MailSettings()
            {
                Host = mailSettingSelected.MailBox,
                Mail = mailSettingSelected.MailAddress,
                Password = mailSettingSelected.MailPassword,
                Port = 25,
            };

            await _mailService.SendEmailAsync(request, mailSetting);

            return new DataResponse<string>(true, null, "ایمیل ارسال شد");
        }

        public async Task<DataResponse<string>>SendSmsAsync(long caseId,string fromSmsCenterId , string message)
        {
            var responsePendingCase = await _pendingCaseRepository.GetByIdAsync(caseId);
            if (responsePendingCase.Success == false || responsePendingCase.Data == null)
                return new DataResponse<string>(new List<string> { "خطا در واکشی اطلاعات" });

            if (string.IsNullOrEmpty(responsePendingCase?.Data?.Mobile))
                return new DataResponse<string>(new List<string> { "موبایل مخاطب یافت نشد" });

            var dataResponseMails = _sourceConfigRepository.GetByAnswerMethodIdAsync((int)AnswerMethod.Sms).Result;
            if (dataResponseMails.Success == false || dataResponseMails.Data == null)
            {
                _logger.LogCritical("GetBySourceTypeId faild reading mail settings {fromMailBox}", fromSmsCenterId);
                return new DataResponse<string>(new List<string> { "تنظیمات مرکز پیامک یافت نشد" });
            }

            var mailSettings = dataResponseMails.Data
                .Where(d=> d.Id.ToString() == fromSmsCenterId )//برای پیامک ایدی رو میگیریم
                .Select(m => funcSourceConfigJson(m.ConfigJson)).ToList();
            var smsSettingSelected = mailSettings.FirstOrDefault();
            if (smsSettingSelected == null)
                return new DataResponse<string>(new List<string> { "تنظیمات مرکز پیامک یافت نشد" });

            if (smsSettingSelected.AllowSend == false)
                return new DataResponse<string>(new List<string> { " مرکز پیامک دسترسی ارسال ندارد" });

            var smsRequest = new SmsRequest
            {
                Destination = responsePendingCase?.Data?.Mobile,
                Body = message,
                SmsCenterPanelNumber = smsSettingSelected.SmsCenterPanelNumber,
                SmsCenterUserName = smsSettingSelected.SmsCenterUserName,
                SmsCenterPassword = smsSettingSelected.SmsCenterPassword,
            };

            await _smsService.SendSmsAsync(smsRequest);

            return new DataResponse<string>(true, null, "پیامک ارسال شد");
        }

        private Func<string, SourceConfigJsonDto> funcSourceConfigJson = (strConfigjson) =>
               string.IsNullOrEmpty(strConfigjson) == true ? null :
               System.Text.Json.JsonSerializer.Deserialize<SourceConfigJsonDto>(strConfigjson);

        private void chooseWayDecorator(AnswerMethod answerMethod)
        {
            switch (answerMethod)
            {
                case AnswerMethod.Email:

                    break;

                case AnswerMethod.Sms:
                    break;
                default:
                    break;
            }
        }
    }
}
