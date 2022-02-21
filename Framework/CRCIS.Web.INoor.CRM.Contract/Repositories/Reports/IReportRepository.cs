using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Dashboard.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Reports
{
    public interface IReportRepository
    {
        Task<DataResponse<ReportLastWeekHistoryDto>> GetCaseHistoryReportAsync();
        Task<DataTableResponse<IEnumerable<PersonReportResponseFullDto>>> GetPersonReport(PersonReportQuery query);
        Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorLockPagingReportAsync(NoorLockReportPagingQuery query);
        Task<DataResponse<NoorLockCaseReportDto>> GetNoorLockReportByRowNumberAsync(NoorLockReportRowNumberQuery query);
        Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorAppPagingReport(NoorLockReportPagingQuery query);
        Task<DataResponse<NoorLockCaseReportDto>> GetNoorAppReportByCaseId(NoorAppReportCaseIdQuery query);
        Task<DataResponse<ReportDashboardDto>> GetTotalCountsReportDashboardAsync();
    }
}
