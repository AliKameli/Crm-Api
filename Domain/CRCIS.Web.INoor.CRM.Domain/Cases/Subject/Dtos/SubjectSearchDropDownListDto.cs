
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Dtos
{
    public class SubjectSearchDropDownListDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Code { get; set; }
        public int Priority { get; set; }
        public long Weight { get; set; }
    }
}
