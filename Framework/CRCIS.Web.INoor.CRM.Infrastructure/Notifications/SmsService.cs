using CRCIS.Web.INoor.CRM.Contract.Notifications;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SmsService(ILoggerFactory loggerFactory, IWebHostEnvironment webHostEnvironment)
        {
            _logger = loggerFactory.CreateLogger<SmsService>();
            _webHostEnvironment = webHostEnvironment;
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

                var url = baseUrl + queryString;

                string str = null;
                if (_webHostEnvironment.IsDevelopment())
                {
                    using var httpClient = new HttpClient();

                    var resposne = await httpClient.GetAsync(url);
                    if (resposne.StatusCode != HttpStatusCode.OK)
                    {
                        _logger.LogWarning(resposne.Content.ToString());
                        return false;
                    }
                    str = await resposne.Content.ReadAsStringAsync();
                }
                else
                {
                    using var handler = new HttpClientHandler
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
                    str = await resposne.Content.ReadAsStringAsync();
                }


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
