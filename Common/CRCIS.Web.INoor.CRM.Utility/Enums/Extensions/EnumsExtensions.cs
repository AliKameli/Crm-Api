using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Enums.Extensions
{
    public static class EnumsExtensions
    {
        public static int ToInt32(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }

        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }
    }
}
