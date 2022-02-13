using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Queris
{
    public class RoleDataTableQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }
        public RoleDataTableQuery(int pageIndex, int pageSize, string sortField, SortOrder? sortOrder)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
        }
    }
}
