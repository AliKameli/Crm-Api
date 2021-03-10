using System;
using System.Collections.Generic;
using System.Text;

namespace CRCIS.Web.INoor.CRM.Contract.Settings
{
    public interface IJwtSettings
    {
        string SecretKey { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        string EncryptKey { get; set; }
        int NotBeforeMinutes { get; set; }
        int ExpirationMinutes { get; set; }
    }
}
