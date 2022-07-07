using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos
{
    public class WarningImportanstOnDayListDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int WarningTypeId { get; set; }
        public string WarningTypeTitle { get; set; }
        public int WarningLevelId { get; set; }
        public string WarningLevelTitle { get; set; }
        public long TotalCount { get; set; }

    }
}
