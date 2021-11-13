using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.Product.Queries
{
    public class ProductDataTableQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }

        public string Title { get; private set; }
        public string ProductIds { get; private set; }
        public string Code { get; private set; }
        public ProductDataTableQuery(int pageIndex, int pageSize,
            string sortField, SortOrder? sortOrder,
            string title, string code)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            Title = title?.Trim();
            Code = code?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
        }
    }
}
