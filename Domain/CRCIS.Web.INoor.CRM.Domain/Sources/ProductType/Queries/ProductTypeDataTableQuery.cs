using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Queries
{
    public class ProductTypeDataTableQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }
        public ProductTypeDataTableQuery(int pageIndex, int pageSize, string sortField, SortOrder? sortOrder)
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