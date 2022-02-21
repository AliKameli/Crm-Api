using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.Dashboard.Dtos
{
    public class ReportDashboardDto
    {
        public long NewCount { get; set; }
        public long CardboardCount { get; set; }
        public long ArchiveCount { get; set; }
        public long TotalCount { get; set; }
        public long ObservingCount { get; set; }

    }
}
