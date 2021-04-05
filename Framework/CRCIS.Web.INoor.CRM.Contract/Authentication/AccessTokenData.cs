using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Authentication
{
    public class AccessTokenData
    {
        public DateTime ExpireAtUtc { get; set; }
        public double ValidToMilliseconds{ get; set; }
        public string AccessToken { get; set; }
        public string RedreshToken { get; set; }
        public string User { get; set; }
    }
 
}