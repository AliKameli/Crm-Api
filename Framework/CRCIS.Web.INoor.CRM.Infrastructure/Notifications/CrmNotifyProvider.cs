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
        private readonly IPendingCaseRepository _pendingCaseRepository;
        private readonly ISourceConfigRepository _sourceConfigRepository;
        private readonly ILogger _logger;

        public CrmNotifyProvider(ILoggerFactory loggerFactory,
            IMailService mailService,
            IPendingCaseRepository pendingCaseRepository,
            ISourceConfigRepository sourceConfigRepository)
        {
            _mailService = mailService;
            _pendingCaseRepository = pendingCaseRepository;
            _sourceConfigRepository = sourceConfigRepository;
            _logger = loggerFactory.CreateLogger<CrmNotifyProvider>();
        }

        public async Task<DataResponse<string>> SendEmailAsync(long caseId, string fromMailBox, string message)
        {
            var responsePendingCase = await _pendingCaseRepository.GetByIdAsync(caseId);
            if (responsePendingCase.Success == false || responsePendingCase.Data == null)
                return new DataResponse<string>(new List<string> { "خطا در واکشی اطلاعات" });

            //var moreDataString = responsePendingCase?.Data?.MoreData;
            //if (string.IsNullOrEmpty(moreDataString))
            //    return new DataResponse<string>(new List<string> { "خطا در واکشی اطلاعات مورد" });

            //var caseMoreDataObject = System.Text.Json.JsonSerializer.Deserialize<ImportCaseMoreDataObject>(moreDataString);
            //if (string.IsNullOrEmpty(caseMoreDataObject?.ToMailBox))
            //    return new DataResponse<string>(new List<string> { "ایمیل مخاطب یافت نشد" });

            if(string.IsNullOrEmpty(responsePendingCase?.Data?.Email))
                return new DataResponse<string>(new List<string> { "ایمیل مخاطب یافت نشد" });

            var dataResponseMails = _sourceConfigRepository.GetBySourceTypesIdAsync(2).Result;
            if (dataResponseMails.Success == false || dataResponseMails.Data == null)
            {
                _logger.LogCritical("GetBySourceTypeId faild reading mail settings {fromMailBox}", fromMailBox);
                return new DataResponse<string>(new List<string> { "تنظیمات ایمیل باکس یافت نشد" });
            }

            Func<string, SourceConfigJsonDto> func = (strConfigjson) =>
                string.IsNullOrEmpty(strConfigjson) == true ? null :
                System.Text.Json.JsonSerializer.Deserialize<SourceConfigJsonDto>(strConfigjson);

            var mailSettings = dataResponseMails.Data.Select(m => func(m.ConfigJson)).ToList();
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


        private void chooseWayDecorator(AnswerMethod answerMethod)
        {
            switch (answerMethod)
            {
                case AnswerMethod.Email:

                    break;
                default:
                    break;
            }
        }
    }
}
