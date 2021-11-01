using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Notify
{
    public class SendNotifyCreateModel
    {
        public long caseId { get; set; }
        public string FromMail { get; set; }
        public string MessageBody { get; set; }

    }
}
