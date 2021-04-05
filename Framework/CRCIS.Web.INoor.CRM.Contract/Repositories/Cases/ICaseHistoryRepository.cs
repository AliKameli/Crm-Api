using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface ICaseHistoryRepository : IBaseRepository
    {
        Task<DataResponse<IEnumerable<CaseHistoriesDto>>> GetReportForCaseAsync(long id);
    }
}
