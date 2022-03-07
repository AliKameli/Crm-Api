using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Answer.Queries
{
    public class AnswerReportQuery : AbstractDataTableQuery
    {
        public string Order { get; private set; }

        public string SourceTypeIds { get; private set; }
        public string ProductIds { get; private set; }
        public string Title { get; private set; }
        public string Global { get; private set; }
        public DateTime? FromDate { get; private set; }
        public DateTime? ToDate { get; private set; }
        public string AnswerMethodIds { get; private set; }

        public AnswerReportQuery(int pageIndex, int pageSize,
         string sortField, SortOrder? sortOrder, string sourceTypeIds, string productIds,string answerMethodIds,
         string title,
         string global, string range)
         : base(pageIndex, pageSize)
        {
            sortField = sortField?.Trim();
            if (!string.IsNullOrEmpty(sortField) && sortField != null)
            {
                Order = $"{sortField} {sortOrder.ToString()}";
            }
            SourceTypeIds = sourceTypeIds?.Trim();
            ProductIds = productIds?.Trim();
            AnswerMethodIds = answerMethodIds?.Trim();
            Title = title?.Trim();

            Global = global?.Trim();

            if (!string.IsNullOrEmpty(range))
            {
                var sDates = range.Split(',');
                FromDate = DateTime.Parse(sDates[0]);
                ToDate = DateTime.Parse(sDates[1]);
            }
        }
    }
}
