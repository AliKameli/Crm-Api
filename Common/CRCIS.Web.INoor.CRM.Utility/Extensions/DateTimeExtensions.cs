using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersinDateString(this DateTime dateTime,bool includeHoursMinutes = false,bool includeSeconds =false )
        {
            var persianCalendar = new System.Globalization.PersianCalendar();

            var stringBuilder = new StringBuilder();
            stringBuilder
                .Append(persianCalendar.GetYear(dateTime))
                .Append('/')
                .Append(persianCalendar.GetMonth(dateTime))
                .Append('/')
                .Append(persianCalendar.GetDayOfMonth(dateTime));
            if (includeHoursMinutes)
            {
                stringBuilder.Append(' ').Append(dateTime.Hour).Append(':').Append(dateTime.Minute);
            }
            if (includeSeconds)
            {
                stringBuilder.Append(' ').Append(dateTime.Second);
            }
            var persianDate = stringBuilder.ToString();
            return persianDate;
        }
    }
}
