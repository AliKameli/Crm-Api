using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Queries
{
    public class CommonAnswerDataTableQuery : AbstractDataTableQuery
    {
        public string SearchWord { get; private set; }
        public string Order { get; private set; }
        public CommonAnswerDataTableQuery(int pageIndex, int pageSize, string searchWord, string sortField, SortOrder? sortOrder)
            : base(pageIndex, pageSize)
        {
            SearchWord = (searchWord is null) ? string.Empty : searchWord;

            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
            else
            {
                Order = "Priority Asc";
            }
        }
    }
}
