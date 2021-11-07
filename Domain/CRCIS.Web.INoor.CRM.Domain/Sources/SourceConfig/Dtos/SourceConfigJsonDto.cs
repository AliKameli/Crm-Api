using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos
{
    public class SourceConfigJsonDto
    {
        public string MailBox { get; set; }
        public string MailAddress { get; set; }
        public string MailPassword { get; set; }

        public string SmsCenterPanelNumber { get; set; }
        public string SmsCenterUserName { get; set; }
        public string SmsCenterPassword { get; set; }

        public bool AllowRead { get; set; }
        public bool AllowSend { get; set; }
    }
}
