using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports
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

    public class PersonReportResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string ProductTitle { get; set; }
        public long RowNumber { get; set; }
        public string SourceTypeTitle { get; set; }
        public int TblNumber { get; set; }
        public string TblName { get; set; }
        public long TotalCount { get; set; }
    }
}
