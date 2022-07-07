using CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.Warnings;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Commands;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Alarms.Warnings
{
    public class WarningRepository : BaseRepository, IWarningRepository
    {
        protected override string TableName => "Warning";
        private ILogger _logger;
        public WarningRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory) : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<WarningRepository>();
        }

        public async Task<DataResponse<long>> CreateAsync(WarningCreateCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Create");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<long>(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<long>(errors);
                return result;
            }
        }


        public async Task<DataTableResponse<IEnumerable<WarningGetDto>>> GetAsync(WarningDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<WarningGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<WarningGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<WarningGetDto>>(errors);
                return result;
            }
        }

        public async Task<DataTableResponse<IEnumerable<WarningImportanstOnDayListDto>>> GetImportantWarningsDayAsync(ImportantWarningsDayQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "ImportantWarningsDay");

                var list =
                     await dbConnection
                    .QueryAsync<WarningImportanstOnDayListDto>(sql, query, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<WarningImportanstOnDayListDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<WarningImportanstOnDayListDto>>(errors);
                return result;
            }
        }
      

    }
}
