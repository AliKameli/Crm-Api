using CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Reports
{
    public interface IReportRepository : IBaseRepository
    {
        Task<DataResponse<CaseHistoryChartDto>> GetCaseHistoryReportAsync();
    }
}
