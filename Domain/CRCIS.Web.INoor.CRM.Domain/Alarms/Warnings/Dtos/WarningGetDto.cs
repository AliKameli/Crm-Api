using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos
{
    public class WarningGetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime VisitDate { get; set; }
        public int WarningTypeId { get; set; }
        public long TotalCount { get; set; }
    }
}
