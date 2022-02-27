using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient.Dtos.SearchUserDto
{
    public class SearchUserOutputDto
    {
        public bool IsSuccess { get; set; }
        public int TotalCount { get; set; }
        //public float Took { get; set; }

        public List<RedisUserDto> Data { get; set; }
    }


    public class RedisUserDto
    {
        public string key { get; set; }
        public RedisUserInfoDto Value { get; set; }

    }

    public class RedisUserInfoDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
