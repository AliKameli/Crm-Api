using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAction.Dtos;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.Role
{
    public class RoleActionRepository : BaseRepository, IRoleActionRepository
    {
        protected override string TableName => "RoleAction";
        private readonly ILogger _logger;
        public RoleActionRepository(ILoggerFactory loggerFactory, ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<RoleActionRepository>();
        }

        public async Task<DataTableResponse<IEnumerable<RoleActionGetDto>>> GetActionsInRoleAsync(int roleId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetByRoleId");

                var list =
                     await dbConnection
                    .QueryAsync<RoleActionGetDto>(sql, new { RoleId = roleId }, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<RoleActionGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<RoleActionGetDto>>(errors);
                return result;
            }

        }

        public async Task<DataResponse<int>> UpdateRoleActionAsync(RoleActionUpdateCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Update");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<int>(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }

    }
}
