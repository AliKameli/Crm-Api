using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Authentication
{
    public interface IJwtProvider
    {
        Task<(DateTime, string)> GenerateAsync(AdminModel user);
    }
}
