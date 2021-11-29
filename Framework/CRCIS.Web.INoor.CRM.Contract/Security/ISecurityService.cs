using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Security
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
        Guid CreateCryptographicallySecureGuid();
        bool IsEquals(string planText, string hashed);
        byte[] GetBytSha256(string input);
        string GetSha256HashHex(string input);
    }
}
