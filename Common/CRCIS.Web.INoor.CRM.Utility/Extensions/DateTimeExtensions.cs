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
                .Append(persianCalendar.GetMonth(dateTime).ToString("00"))
                .Append('/')
                .Append(persianCalendar.GetDayOfMonth(dateTime).ToString("00"));
            if (includeHoursMinutes)
            {
                stringBuilder.Append(' ').Append(dateTime.Hour.ToString("00"))
                    .Append(':')
                    .Append(dateTime.Minute.ToString("00"));
            }
            if (includeSeconds)
            {
                stringBuilder.Append(':').Append(dateTime.Second.ToString("00"));
            }
            var persianDate = stringBuilder.ToString();
            return persianDate;
        }
    }
}
