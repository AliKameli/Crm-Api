using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Enums.Extensions
{
    public static class EnumsExtensions
    {
        public static int ToInt(Enum e)
        {
            return Convert.ToInt32(e);
        }
    }
}
