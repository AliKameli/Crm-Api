using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Commands
{
    public class SourceTypeUpdateCommand
    {
        public int Id { get;private set; }
        public string Title { get;private set; }
        public SourceTypeUpdateCommand(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
