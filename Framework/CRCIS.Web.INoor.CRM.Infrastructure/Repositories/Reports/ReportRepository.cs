using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports;
using CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Person.Queries;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Reports;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries;
using Microsoft.Extensions.Logging;
using CRCIS.Web.INoor.CRM.Domain.Reports.Dashboard.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Case.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Case.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Operator.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Subject.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Reports.Subject.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Answer.Queries;
using CRCIS.Web.INoor.CRM.Domain.Reports.Answer.Dtos;
using System.Drawing;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Reports
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        protected override string TableName => "Report";
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;
        private readonly ILogger _logger;
        public ReportRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory,
            IMapper mapper, IIdentity identity) : base(sqlConnectionFactory)
        {
            _mapper = mapper;
            _identity = identity;
            _logger = loggerFactory.CreateLogger<ReportRepository>();
        }

        public async Task<DataResponse<ReportLastDaysCaseHistoryDto>> GetCaseHistoryReportAsync()
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "LastWeekCaseHistory");

                var list =
                     await dbConnection
                    .QueryAsync<CaseHistoryLastDaysDto>(sql, commandType: CommandType.StoredProcedure);

                var days = list.Select(a => a.Day).Distinct().OrderBy(a => a).ToList();
                var colors = new List<string> { "#6e18f5", "#6e28f5", "#6e38f5", "#6e48f5", "#6e58f5", "#6ed8f5", "#6ed8f5", "#6ed8f5", "#00bb7e", "#ef6262", "#e262a2", "#f2ea00", "#000cf2", "#100cf2", "#200cf2", "#300cf2", "#400cf2" };
                var types = list.Select(a => a.OperationTypeId).Distinct().OrderBy(a => a);

                IList<CaseHistoryLastWeekChartDto> finalResult = new List<CaseHistoryLastWeekChartDto>();
                for (int index = 0; index < types.Count(); index++)
                {
                    var type = types.ElementAt(index);

                    var color = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), 0).ToString();

                    try { color = colors.ElementAt(index); } catch { }
                    var chartDto = new CaseHistoryLastWeekChartDto()
                    {
                        TypeId = type,
                        Label = list.First(a => a.OperationTypeId == type)?.OperationTypeTitle,
                        Fill = false,
                        BackgroundColor = color,
                        BorderColor = color,
                        Data = new List<int>(),
                    };

                    foreach (var day in days)
                    {
                        var record = list.FirstOrDefault(a => a.OperationTypeId == type && a.Day == day);
                        var count = (record?.CNT).GetValueOrDefault();
                        chartDto.Data.Add(count);
                    }
                    finalResult.Add(chartDto);
                }

                var firstDate = new DateTime(DateTime.Now.Year, 1, 1);
                var historyChartDto = new ReportLastDaysCaseHistoryDto
                {
                    DayNumbers = days.Select(d => firstDate.AddDays(d - 1).ToPersinDateString()).ToList(),
                    Datasets = finalResult
                };

                var result = new DataResponse<ReportLastDaysCaseHistoryDto>(historyChartDto);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<ReportLastDaysCaseHistoryDto>(errors);
                return result;
            }
        }

        public async Task<DataResponse<ReportLastDaysProductHistoryDto>> GetProductHistoryReportAsync()
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "LastWeekProductHistory");

                var list =
                     await dbConnection
                    .QueryAsync<ProductHistoryLastDaysDto>(sql, commandType: CommandType.StoredProcedure);

                var days = list.Select(a => a.Day).Distinct().OrderBy(a => a).ToList();
                var colors = new List<string> { "#6e18f5", "#6e28f5", "#6e38f5", "#6e48f5", "#6e58f5", "#6ed8f5", "#6ed8f5", "#6ed8f5", "#00bb7e", "#ef6262", "#e262a2", "#f2ea00", "#000cf2", "#100cf2", "#200cf2", "#300cf2", "#400cf2" };
                var types = list.Select(a => a.ProductId).Distinct().OrderBy(a => a);

                IList<ProductHistoryLastDayChartDto> finalResult = new List<ProductHistoryLastDayChartDto>();
                Random r = new Random();
                for (int index = 0; index < types.Count(); index++)
                {
                    var type = types.ElementAt(index);
                    var color =  Color.FromArgb(r.Next(0, 256), r.Next(0, 256), 0).ToString();

                    try { color = colors.ElementAt(index); } catch { }
                   
                    var chartDto = new ProductHistoryLastDayChartDto()
                    {
                        TypeId = type,
                        Label = list.First(a => a.ProductId == type)?.ProductTitle,
                        Fill = false,
                        BackgroundColor = color,
                        BorderColor = color,
                        Data = new List<int>(),
                    };

                    foreach (var day in days)
                    {
                        var record = list.FirstOrDefault(a => a.ProductId == type && a.Day == day);
                        var count = (record?.CNT).GetValueOrDefault();
                        chartDto.Data.Add(count);
                    }
                    finalResult.Add(chartDto);
                }

                var firstDate = new DateTime(DateTime.Now.Year, 1, 1);
                var historyChartDto = new ReportLastDaysProductHistoryDto
                {
                    DayNumbers = days.Select(d => firstDate.AddDays(d - 1).ToPersinDateString()).ToList(),
                    Datasets = finalResult
                };

                var result = new DataResponse<ReportLastDaysProductHistoryDto>(historyChartDto);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<ReportLastDaysProductHistoryDto>(errors);
                return result;
            }
        }

        public async Task<DataResponse<ReportDashboardDto>> GetTotalCountsReportDashboardAsync()
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Dashboard");

                var dto =

                await dbConnection
                  .QueryFirstOrDefaultAsync<ReportDashboardDto>(sql, new { }, commandType: CommandType.StoredProcedure);

                if (dto == null)
                {
                    return new DataResponse<ReportDashboardDto>(false);
                }

                var result = new DataResponse<ReportDashboardDto>(dto);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<ReportDashboardDto>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<PersonReportResponseFullDto>>> GetPersonReport(PersonReportQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "PersonFiltered");

                var list =
                     await dbConnection
                    .QueryAsync<PersonReportDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list
                    .Select(r => (r == null) ? null :
                        _mapper.Map<PersonReportResponseFullDto>(r)
                    )
                    .Select(r => (r == null) ? null :
                            r.PairCommandAccess(_identity.GetAdminId())
                    );

                var result = new DataTableResponse<IEnumerable<PersonReportResponseFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<PersonReportResponseFullDto>>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<ReportCaseResponseFullDto>>> GetCaseReportAsync(CaseReportQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Case");

                var list =
                     await dbConnection
                    .QueryAsync<ReportCaseDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list
                    .Select(r => (r == null) ? null :
                        _mapper.Map<ReportCaseResponseFullDto>(r)
                    )
                    .Select(r => (r == null) ? null :
                            r.PairCommandAccess(_identity.GetAdminId())
                    );

                var result = new DataTableResponse<IEnumerable<ReportCaseResponseFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ReportCaseResponseFullDto>>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorAppPagingReport(NoorLockReportPagingQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "NoorAppPaging");

                var list =
                     await dbConnection
                    .QueryAsync<NoorLockCaseReportDto>(sql, query, commandType: CommandType.StoredProcedure);
                list = list.Select(d => d.PairNoorLockCaseReportNotHtmlAnswer());
                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var result = new DataTableResponse<IEnumerable<NoorLockCaseReportDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<NoorLockCaseReportDto>>(errors);
                return result;
            }

        }

        public async Task<DataResponse<NoorLockCaseReportDto>> GetNoorAppReportByCaseId(NoorAppReportCaseIdQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "NoorAppGetByCaseId");

                var data =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<NoorLockCaseReportDto>(sql, query, commandType: CommandType.StoredProcedure);
                if (data != null)
                {
                    data = data.PairNoorLockCaseReportNotHtmlAnswer();
                    return new DataResponse<NoorLockCaseReportDto>(data);
                }

                var errors = new List<string> { " مورد یافت نشد" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<NoorLockCaseReportDto>>> GetNoorLockPagingReportAsync(NoorLockReportPagingQuery query)
        {

            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "NoorlockPaging");

                var list =
                     await dbConnection
                    .QueryAsync<NoorLockCaseReportDto>(sql, query, commandType: CommandType.StoredProcedure);

                list = list.Select(d => d.PairNoorLockCaseReportNotHtmlAnswer());
                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var result = new DataTableResponse<IEnumerable<NoorLockCaseReportDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<NoorLockCaseReportDto>>(errors);
                return result;
            }

        }

        public async Task<DataResponse<NoorLockCaseReportDto>> GetNoorLockReportByRowNumberAsync(NoorLockReportRowNumberQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "NoorlockGetByRowNumber");

                var data =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<NoorLockCaseReportDto>(sql, query, commandType: CommandType.StoredProcedure);
                if (data != null)
                {
                    data = data.PairNoorLockCaseReportNotHtmlAnswer();
                    return new DataResponse<NoorLockCaseReportDto>(data);
                }

                var errors = new List<string> { " مورد یافت نشد" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<ReportOperatorResponseFullDto>>> GetOperatorReportAsync(OperatorReportQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Operator");

                var list =
                     await dbConnection
                    .QueryAsync<ReportOperatorDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list
                    .Select(r => (r == null) ? null :
                        _mapper.Map<ReportOperatorResponseFullDto>(r)
                    );

                var result = new DataTableResponse<IEnumerable<ReportOperatorResponseFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ReportOperatorResponseFullDto>>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<ReportSubjectResponseFullDto>>> GetSubjectReportAsync(SubjectReportQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Subject");

                var list =
                     await dbConnection
                    .QueryAsync<ReportSubjectDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list
                    .Select(r => (r == null) ? null :
                        _mapper.Map<ReportSubjectResponseFullDto>(r)
                    );

                var result = new DataTableResponse<IEnumerable<ReportSubjectResponseFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ReportSubjectResponseFullDto>>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<ReportAnswerResponseFullDto>>> GetAnsweringReportAsync(AnswerReportQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Answering");

                var list =
                     await dbConnection
                    .QueryAsync<ReportAnswerDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list
                    .Select(r => (r == null) ? null :
                        _mapper.Map<ReportAnswerResponseFullDto>(r)
                    );

                var result = new DataTableResponse<IEnumerable<ReportAnswerResponseFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ReportAnswerResponseFullDto>>(errors);
                return result;
            }
        }
    }
}