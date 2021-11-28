using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos
{
    public class NoorLockAppKeyDto
    {
        public string NoorLockSk { get; set; }
        public long? NoorLockSnId { get; set; }
        public string NoorLockActivationCode { get; set; }
        public string NoorLockTypeOfComment { get; set; }
    }
}

