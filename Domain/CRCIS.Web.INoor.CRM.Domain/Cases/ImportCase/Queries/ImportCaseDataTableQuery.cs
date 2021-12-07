using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Queries
{
    public class ImportCaseDataTableQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }

        public string SourceTypeIds { get; private set; }
        public string ProductIds { get;private set; }
        //public string FirstSubject { get;private set; }
        public string Title { get; private set; }
        public ImportCaseDataTableQuery(int pageIndex, int pageSize,
            string sortField, SortOrder? sortOrder,
            string sourceTypeTitle,string productTitle, string title)
            : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }

            SourceTypeIds = sourceTypeTitle?.Trim();
            ProductIds = productTitle?.Trim();
            Title = title?.Trim();
        }
    }
}
