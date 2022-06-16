using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Users.UserSearch
{
    public class SearchUserOutputDto
    {
        public bool IsSuccess { get; set; }
        public int TotalCount { get; set; }
        public List<SeadrchUserDto> Data { get; set; }
    }

    public class SeadrchUserDto
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

    }


}
