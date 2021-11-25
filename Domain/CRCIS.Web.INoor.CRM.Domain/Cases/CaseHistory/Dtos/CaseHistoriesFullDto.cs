using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos
{
    public class CaseHistoriesFullDto
    {
        public IEnumerable<CaseHistoriesDto> CaseHistoriesNoAnswer { get; set; }
        public IEnumerable<CaseHistoriesDto> CaseHistoriesAnswer { get; set; }
    }

}
