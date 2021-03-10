using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands
{
    public class CaseStatusUpdateCommand
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        public CaseStatusUpdateCommand(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
