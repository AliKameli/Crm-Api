using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Extensions
{
    public static class StringExtensions
    {
        internal static int Year(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[0]}{arr[1]}{arr[2]}{arr[3]}";
            return Convert.ToInt32(year);
        }
        internal static int Month(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[4]}{arr[5]}";
            return Convert.ToInt32(year);
        }
        internal static int Day(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[6]}{arr[7]}";
            return Convert.ToInt32(year);
        }
        internal static int Hour(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[8]}{arr[9]}";
            return Convert.ToInt32(year);
        }
        internal static int Minute(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[10]}{arr[11]}";
            return Convert.ToInt32(year);
        }
        internal static int Second(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[12]}{arr[13]}";
            return Convert.ToInt32(year);
        }
        internal static int Millisecond(this string str)
        {
            var arr = str.ToCharArray();
            var year = $"{arr[14]}{arr[15]}{arr[16]}";
            return Convert.ToInt32(year);
        }

        public static DateTime GetMailDate(this string str)
        {
            return new DateTime(year: str.Year(), month: str.Month(), day: str.Day(),
                                hour: str.Hour(), minute: str.Minute(), second: str.Second(),
                                millisecond: str.Millisecond());
        }
    }
}
