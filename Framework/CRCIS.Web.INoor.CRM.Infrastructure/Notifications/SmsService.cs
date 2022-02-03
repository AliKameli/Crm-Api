﻿using CRCIS.Web.INoor.CRM.Contract.Notifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Notifications
{
    public class SmsService : ISmsService
    {
        private readonly ILogger _logger;
        public SmsService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SmsService>();
        }
        public async Task<bool> SendSmsAsync(SmsRequest message)
        {
            //var result = new ServiceResponse<bool>();
            try
            {
                var baseUrl = "http://www.tsms.ir/url/tsmshttp.php?";
                var queryString =
                   $"from={message.SmsCenterPanelNumber}" +
                   $"&username={message.SmsCenterUserName}" +
                   $"&password={message.SmsCenterPassword}" +
                   $"&to={message.ToMobile}" +
                   $"&message={message.Body}";

                var url = baseUrl + HttpUtility.UrlEncode(queryString);
                //var ocRequest = System.Net.WebRequest.Create(url);
                //ocRequest.Timeout = 30000;
                //var res = ocRequest.GetResponse();

                var handler = new HttpClientHandler
                {
                    Proxy = new WebProxy()
                    {
                        Address = new Uri("http://172.16.20.207:3128"),
                        BypassProxyOnLocal = true,
                        UseDefaultCredentials = false,
                    }
                };

                using var httpClient = new HttpClient(handler);

                var resposne = await httpClient.GetAsync(url);
                if (resposne.StatusCode != HttpStatusCode.OK)
                {
                    _logger.LogWarning(resposne.Content.ToString());
                    return false;
                }
                var str = await resposne.Content.ReadAsStringAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                _logger.LogError(ex?.InnerException?.Message);
                _logger.LogError(ex?.InnerException?.StackTrace);
                return false;
            }

        }
    }
}
