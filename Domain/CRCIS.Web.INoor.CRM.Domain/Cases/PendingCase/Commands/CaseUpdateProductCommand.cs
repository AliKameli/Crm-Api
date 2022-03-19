using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands
{
    public class CaseUpdateProductCommand
    {
        public int ProductId { get; set; }
        public long  CaseId { get; set; }
    }
}
