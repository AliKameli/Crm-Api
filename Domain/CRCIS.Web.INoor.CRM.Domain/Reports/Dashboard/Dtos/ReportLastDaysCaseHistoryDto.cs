using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Dashboard.Dtos
{
   public class ReportLastDaysCaseHistoryDto
    {
        public List<string> DayNumbers { get; set; }
        public IEnumerable<CaseHistoryLastWeekChartDto> Datasets { get; set; }
    }
    public class CaseHistoryLastWeekChartDto
    {
        public int TypeId { get; set; }
        public string Label { get; set; }
        public bool Fill { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public List<int> Data { get; set; }
    }
}
