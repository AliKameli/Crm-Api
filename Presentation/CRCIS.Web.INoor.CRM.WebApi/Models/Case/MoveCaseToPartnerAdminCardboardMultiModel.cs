using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Case
{
    public class MoveCaseToPartnerAdminCardboardMultiModel
    {
        public int AdminId { get; set; }
        public List<long> CaseIds { get; set; }
    }
}
