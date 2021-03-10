using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Commands
{
    public class OperationTypeUpdateCommand
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        public OperationTypeUpdateCommand(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
