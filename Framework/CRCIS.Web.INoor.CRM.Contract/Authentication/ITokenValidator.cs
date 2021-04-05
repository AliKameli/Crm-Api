using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Authentication
{
    public interface ITokenValidator
    {
        Task ValidateAsync(TokenValidatedContext context);
    }
}
