﻿using AutoMapper;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Reports
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        protected override string TableName => "Report";
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;
        public ReportRepository(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper, IIdentity identity) : base(sqlConnectionFactory)
        {
            _mapper = mapper;
            _identity = identity;
        }

        public async Task<DataResponse<CaseHistoryChartDto>> GetCaseHistoryReportAsync()
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "LastWeekCaseHistory");

                var list =
                     await dbConnection
                    .QueryAsync<CaseHistoryLastWeekDto>(sql, commandType: CommandType.StoredProcedure);

                var days = list.Select(a => a.Day).Distinct().OrderBy(a => a).ToList();
                var colors = new List<string> { "#2f4860", "#00bb7e", "#ef6262", "#e262a2", "#0f6262", "#5562a2" };
                var types = list.Select(a => a.OperationTypeId).Distinct().OrderBy(a => a);

                IList<CaseHistoryLastWeekChartDto> finalResult = new List<CaseHistoryLastWeekChartDto>();
                for (int index = 0; index < types.Count(); index++)
                {
                    var type = types.ElementAt(index);
                    var color = colors.ElementAt(index);
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
                var historyChartDto = new CaseHistoryChartDto
                {
                    DayNumbers = days.Select(d => firstDate.AddDays(d - 1).ToPersinDateString()).ToList(),
                    Datasets = finalResult
                };

                var result = new DataResponse<CaseHistoryChartDto>(historyChartDto);
                return result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<CaseHistoryChartDto>(errors);
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
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<PersonReportResponseFullDto>>(errors);
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

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var result = new DataTableResponse<IEnumerable<NoorLockCaseReportDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
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
                    return new DataResponse<NoorLockCaseReportDto>(data);
                }

                var errors = new List<string> { " مورد یافت نشد" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
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

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var result = new DataTableResponse<IEnumerable<NoorLockCaseReportDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
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
                    return new DataResponse<NoorLockCaseReportDto>(data);
                }

                var errors = new List<string> { " مورد یافت نشد" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<NoorLockCaseReportDto>(errors);
                return result;
            }
        }

    }
}

