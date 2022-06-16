using CRCIS.Web.INoor.CRM.Domain.Users.UserSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.ElkSearch
{
    public interface IUserSearch
    {
        Task<SearchUserOutputDto> SearchAsync(string fullName, string mobile, string username, string email);
    }
}
