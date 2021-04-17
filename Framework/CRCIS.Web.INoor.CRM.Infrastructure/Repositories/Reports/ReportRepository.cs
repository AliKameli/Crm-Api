using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Reports.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Reports
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        protected override string TableName => "Report";
        public ReportRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
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
                throw ex;
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<CaseHistoryChartDto>(errors);
                return result;
            }
        }
    }
}
