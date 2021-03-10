using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Commands
{
    public class OperationTypeCreateCommand
    {
        public string Title { get; private set; }

        public OperationTypeCreateCommand(string title)
        {
            Title = title;
        }
    }
}
