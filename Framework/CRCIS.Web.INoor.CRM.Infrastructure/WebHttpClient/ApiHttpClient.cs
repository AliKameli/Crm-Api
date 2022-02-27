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

        private async Task<HttResultDto<TOutoput>> PostInputAsync<TOutoput>(string url, object input) where TOutoput : class
        {

            var resultDto = new HttResultDto<TOutoput>();
            var httpResponse = await _httpClient.PostAsJsonAsync(url, input);
            if (httpResponse.IsSuccessStatusCode == false)
            {
                resultDto.Faild(httpResponse.StatusCode);
            }
            var json = await httpResponse.Content.ReadAsStringAsync();
            var data = System.Text.Json.JsonSerializer.Deserialize<TOutoput>(json,options: new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            resultDto.Success(data);

            return resultDto;
        }


        public async Task<IEnumerable<SearchUserOutputDto>> GetUsersItemsSearchAsync(SearchUserItemSearcInputDto input)
        {
            var result = await this.PostInputAsync<IEnumerable<SearchUserOutputDto>>($"{RedisSearcBaseUrl}/api/v10/search/user", input);
            if (result.IsSuccess == false)
            {
                return new List<SearchUserOutputDto>();
            }

            return result.Data;
        }

        public async Task<IEnumerable<RedisUserDto>> GetUsersGlobalSearchAsync(SearchUserGlobalSearchInputDto input)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("SearchText", input.SearchText);

            var result = await this.PostInputAsync<SearchUserOutputDto>($"{RedisSearcBaseUrl}/api/v10/search/user", dic);
            if (result.IsSuccess == false)
            {
                return new List<RedisUserDto>();
            }

            return result.Data.Data;
        }
    }
}
