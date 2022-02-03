using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.NoorlockSoftwares
{
    public class NoorlockSoftwareInserted
    {
        public string JsonString { get; set; }
    }
    public class NoorlockSoftwareInsertedModel
    {
        public int NoorlockSoftwareCode { get; set; }
        public string CrmSoftwareSecret { get; set; }
        public string SotwareName { get; set; }
    }
}
