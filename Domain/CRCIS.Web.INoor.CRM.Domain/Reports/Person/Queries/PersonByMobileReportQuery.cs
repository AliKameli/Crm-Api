using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries
{
    public class PersonByMobileReportQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }
        public string Mobile { get; private set; }
        public PersonByMobileReportQuery(int pageIndex, int pageSize,
            string sortField, SortOrder? sortOrder, string mobile)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
            Mobile = mobile?.Trim();
            //if (string.IsNullOrEmpty(Mobile) == false)
            //{
            //    if (Mobile.StartsWith("+98-9"))
            //    {
            //        var temp = Mobile.Substring("+98-9".Length);
            //        Mobile = Mobile?.Replace("+98-9", "9");
            //    }

            //    if (Mobile.StartsWith("00989"))
            //    {
            //        Mobile = Mobile?.Replace("00989", "9")  ;
            //    }

            //    if (Mobile.StartsWith("0989"))
            //    {

            //    }
            //        Mobile = Mobile?.Replace("0989", "9")      ;

            //    if (Mobile.StartsWith("+98-9"))
            //        Mobile = Mobile?.Replace("09", "9");

            //}

        }
    }
}
