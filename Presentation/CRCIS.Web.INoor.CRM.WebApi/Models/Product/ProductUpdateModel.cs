using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Product
{
    public class ProductUpdateModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductTypeId { get; set; }

    }
}
