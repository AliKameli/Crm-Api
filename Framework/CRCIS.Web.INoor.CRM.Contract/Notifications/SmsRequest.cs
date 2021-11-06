using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Notifications
{
  public  class SmsRequest
    {
        public string Destination { get; set; }
        public string Body { get; set; }
    }
}
