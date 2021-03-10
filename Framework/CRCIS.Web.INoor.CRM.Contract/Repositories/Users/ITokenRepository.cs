using CRCIS.Web.INoor.CRM.Domain.Users.Token.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Users
{
    public interface ITokenRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(TokenCreateCommand command);
    }
}
