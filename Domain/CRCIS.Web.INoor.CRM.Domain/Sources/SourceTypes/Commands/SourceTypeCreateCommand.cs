using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Commands
{
    public class SourceTypeCreateCommand
    {
        public string Title { get;private set; }

        public SourceTypeCreateCommand(string title)
        {
            Title = title;
        }
    }
}
