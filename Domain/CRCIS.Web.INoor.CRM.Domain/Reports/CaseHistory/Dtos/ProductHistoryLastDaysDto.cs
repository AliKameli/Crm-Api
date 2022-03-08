using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos
{
    public class ProductHistoryLastDaysDto
    {
        public int Day { get; set; }
        public int CNT { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
    }
}
