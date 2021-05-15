using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Account
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class LoginVerifyTokenModel
    {
       
        public string VerifyToken { get; set; }

    }
}
