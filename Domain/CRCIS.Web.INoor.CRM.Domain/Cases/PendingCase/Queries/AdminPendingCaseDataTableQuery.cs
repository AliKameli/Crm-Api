using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries
{
    public class AdminPendingCaseDataTableQuery : AbstractDataTableQuery
    {

        public int AdminId { get;private set; }
        public string Order { get; private set; }
        public AdminPendingCaseDataTableQuery(int pageIndex, int pageSize, 
            string sortField, SortOrder? sortOrder,
            int adminId)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
            AdminId = adminId;
        }

    }
}
