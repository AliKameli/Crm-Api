using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries
{

    public class PersonReportQuery : AbstractDataTableQuery
    {

        public Guid NoorUserId { get; private set; }
        public string Order { get; private set; }
        public PersonReportQuery(int pageIndex, int pageSize, string userId,
            string sortField, SortOrder? sortOrder)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
            NoorUserId = Guid.Parse(userId);
        }

    }
}
