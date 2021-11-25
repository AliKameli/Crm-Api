using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Commands
{
    public class AnsweringUpdateResultCommand
    {
        public long PendingHistoryId { get; set; }
        public int SourceConfigId { get; set; }
        public bool Result { get; set; }
    }
}
