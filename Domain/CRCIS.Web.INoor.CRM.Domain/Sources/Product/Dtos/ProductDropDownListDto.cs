using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.Product.Dtos
{
    public class ProductDropDownListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ProductTypeId { get; set; }
    }
}
