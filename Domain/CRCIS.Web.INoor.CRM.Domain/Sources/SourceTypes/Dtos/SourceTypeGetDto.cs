using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Dtos
{
    public class SourceTypeGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
    }
}
