using CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient.Dtos;
using CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient.Dtos.SearchUserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient
{
    public class ApiHttpClient
    {
        private const string RedisSearcBaseUrl = "http://172.16.8.53:1010";
        private readonly HttpClient _httpClient;
        public ApiHttpClient()
        {
            _httpClient = new HttpClient();
        }

        private async Task<HttpResultDto<TOutoput>> PostInputAsync<TOutoput>(string url, object input) where TOutoput : class
        {

            var resultDto = new HttpResultDto<TOutoput>();
            var httpResponse = await _httpClient.PostAsJsonAsync(url, input);
            if (httpResponse.IsSuccessStatusCode == false)
            {
                resultDto.Faild(httpResponse.StatusCode);
                return resultDto;
            }
            var json = await httpResponse.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TOutoput>(json,options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            resultDto.Success(data);

            return resultDto;
        }


       
    }
}
