using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.ProductType.Commands
{
    public class ProductTypeCreateCommand
    {
        public string Title { get;private set; }

        public ProductTypeCreateCommand(string title)
        {
            Title = title;
        }
    }
}
