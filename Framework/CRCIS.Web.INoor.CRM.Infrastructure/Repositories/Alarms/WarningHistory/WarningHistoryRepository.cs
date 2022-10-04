using CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.WarningHistory;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Alarms.WarningHistory.Commands;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Alarms.WarningHistory
{
    public class WarningHistoryRepository : BaseRepository, IWarningHistoryRepository
    {
        protected override string TableName => "WarningHistory";

        private ILogger _logger;
        public WarningHistoryRepository(ISqlConnectionFactory sqlConnectionFactory,ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<WarningHistoryRepository>();
        }
        public async Task<DataResponse<long>> CreateAsync(WarningHistoryCreateCommand command)
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


        // public async Task AddLogNotifyWarningToAdmin()
        // {

        // }
        // public async Task AddLogAdminVisitedWarning()
        // {

        // }

    }
}
