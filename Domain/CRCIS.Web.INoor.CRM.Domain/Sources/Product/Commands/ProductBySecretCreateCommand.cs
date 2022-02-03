using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.Product.Commands
{
    public class ProductBySecretCreateCommand
    {
       
        public string Title { get; set; }
        public int Code { get; set; }
        public string Secret { get; set; }
    }
}
