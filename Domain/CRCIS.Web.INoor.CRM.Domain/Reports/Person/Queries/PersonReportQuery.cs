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

        public string SourceTypeIds { get; private set; }
        public string ProductIds { get; private set; }
        //public string FirstSubject { get; set; }
        public string Title { get; private set; }
        public PersonReportQuery(int pageIndex, int pageSize, string userId,
            string sortField, SortOrder? sortOrder, string sourceTypeIds, string productIds, string title)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
            NoorUserId = Guid.Parse(userId);
            SourceTypeIds = sourceTypeIds?.Trim();
            ProductIds = productIds?.Trim();
            Title = title?.Trim();
        }

    }
}
