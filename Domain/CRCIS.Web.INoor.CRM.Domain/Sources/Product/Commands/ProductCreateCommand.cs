using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands
{
    public class ProductCreateCommand
    {
        public string Title { get;private set; }
        public int ProductTypeId { get;private set; }
        public int? Code { get; private set; }

        public ProductCreateCommand(string title, int productTypeId, int? code)
        {
            Title = title;
            ProductTypeId = productTypeId;
            Code = code;
        }
    }
}
