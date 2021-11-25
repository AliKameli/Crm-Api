using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Case
{
    public static class CaseHistoriesDtoSpecifications
    {
        public static CaseHistoriesFullDto PairCaseHistoriesFullDto(this IEnumerable<CaseHistoriesDto> dto)
        {
            var result = new CaseHistoriesFullDto
            {
                CaseHistoriesAnswer = new List<CaseHistoriesDto>(),
                CaseHistoriesNoAnswer = new List<CaseHistoriesDto>(),
            };

            result.CaseHistoriesNoAnswer = dto.Where(a => a.PendingHistoryId is null ).AsEnumerable();
            result.CaseHistoriesAnswer = dto.Where(a => a.PendingHistoryId is not null).AsEnumerable();

            return result;
        }
    }
}
