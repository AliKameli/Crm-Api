using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Notifications
{
    public class SmsRequest
    {
        public string SmsCenterPanelNumber { get; set; }
        public string SmsCenterUserName { get; set; }
        public string SmsCenterPassword { get; set; }

        public string ToMobile { get; set; }
        public string Body { get; set; }
    }
}
