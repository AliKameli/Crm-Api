using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Logs.Commands
{
    public class LogCreateCommand
    {
        public string LogLevel { get;  set; }
        public string Message { get;  set; }
        public string MoreData { get; set; }
        public DateTime CreateAt { get; private set; }
        public int? AdminId { get;  set; }
        public string CategoryName { get; set; }
        public int EventId { get; set; }
        public string LoggerName { get; set; }
        public string Url { get; set; }

        public LogCreateCommand()
        {
            CreateAt = DateTime.Now;
        }
    }

}
