using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Commands
{
    public class SourceConfigCreateCommand
    {
        public string Title { get; private set; }
        public int ProductId { get; private set; }
        public int SourceTypeId { get; private set; }
        public string ConfigJson { get; private set; }

        public SourceConfigCreateCommand(string title, int productId, int sourceTypeId, string configJson)
        {
            Title = title;
            ProductId = productId;
            SourceTypeId = sourceTypeId;
            ConfigJson = configJson;
        }
    }
}
