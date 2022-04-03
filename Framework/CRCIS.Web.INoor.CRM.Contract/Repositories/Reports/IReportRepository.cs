﻿using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.AdminCardboard.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.AdminCardboard.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Answer.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Answer.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Case.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Case.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Dashboard.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Subject.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Subject.Queries;
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
        Task<DataTableResponse<IEnumerable<AdminCardboardFullDto>>> GetAdminCardboardReportAsync(AdminCardboardReportQuery query);
        Task<DataTableResponse<IEnumerable<ReportAnswerResponseFullDto>>> GetAnsweringReportAsync(AnswerReportQuery query);
        Task<DataResponse<ReportLastDaysAnswerResultHistoryDto>> GetAnswerResultHistoryReportAsync();
        Task<DataResponse<ReportLastDaysCaseHistoryDto>> GetCaseHistoryReportAsync();
        Task<DataTableResponse<IEnumerable<ReportCaseResponseFullDto>>> GetCaseReportAsync(CaseReportQuery query);
        Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorAppPagingReport(NoorLockReportPagingQuery query);
        Task<DataResponse<NoorLockCaseReportDto>> GetNoorAppReportByCaseId(NoorAppReportCaseIdQuery query);
        Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorLockPagingReportAsync(NoorLockReportPagingQuery query);
        Task<DataResponse<NoorLockCaseReportDto>> GetNoorLockReportByRowNumberAsync(NoorLockReportRowNumberQuery query);
        Task<DataTableResponse<IEnumerable<ReportOperatorResponseFullDto>>> GetOperatorReportAsync(OperatorReportQuery query);
        Task<DataTableResponse<IEnumerable<PersonReportResponseFullDto>>> GetPersonReport(PersonReportQuery query);
        Task<DataResponse<ReportLastDaysProductHistoryDto>> GetProductHistoryReportAsync();
        Task<DataTableResponse<IEnumerable<ReportSubjectResponseFullDto>>> GetSubjectReportAsync(SubjectReportQuery query);
        Task<DataResponse<ReportDashboardDto>> GetTotalCountsReportDashboardAsync();
    }
}
