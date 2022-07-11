using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Commands
{
    public class WarningCreateCommand
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public int WarningTypeId { get; set; }

        public WarningCreateCommand(string title, int warningTypeId)
        {
            Title = title;
            WarningTypeId = warningTypeId;
            CreateDate = DateTime.Now;
        }
    }
}
