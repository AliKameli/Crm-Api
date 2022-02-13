using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.AdminAction;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Permissions.AdminAction.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.AdminAction
{
    public class AdminActionRepository : BaseRepository, IAdminActionRepository
    {
        protected override string TableName => "AdminAction";
        private readonly ILogger _logger;
        public AdminActionRepository(ILoggerFactory loggerFactory ,ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<AdminActionRepository>();
        }

        public async Task<DataTableResponse<IEnumerable<AdminActionDto>>> GetAdminActionByAdminIdAsync(int adminId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<AdminActionDto>(sql, new { AdminId = adminId}, commandType: CommandType.StoredProcedure);
                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<AdminActionDto>>(list,totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<AdminActionDto>>(errors);
                return result;
            }
        }


       
    }
}
