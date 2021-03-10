using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands
{
    public class ProductUpdateCommand
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int ProductTypeId { get; private set; }

        public ProductUpdateCommand(int id, string title, int productTypeId)
        {
            Id = id;
            Title = title;
            ProductTypeId = productTypeId;
        }
    }
}
