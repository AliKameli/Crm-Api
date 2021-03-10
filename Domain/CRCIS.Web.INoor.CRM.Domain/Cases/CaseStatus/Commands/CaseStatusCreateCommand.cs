using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands
{
    public class CaseStatusCreateCommand
    {
        public string Title { get; private set; }

        public CaseStatusCreateCommand(string title)
        {
            this.Title = title;
        }
    }
}
