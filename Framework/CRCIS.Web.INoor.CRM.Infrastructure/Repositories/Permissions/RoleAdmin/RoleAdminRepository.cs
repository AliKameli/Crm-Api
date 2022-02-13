using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.RoleAdmin;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Dtos;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.RoleAdmin
{
    public class RoleAdminRepository : BaseRepository, IRoleAdminRepository
    {
        protected override string TableName => "RoleAdmin";
        private readonly ILogger _logger;
        public RoleAdminRepository(ILoggerFactory loggerFactory, ISqlConnectionFactory sqlConnectionFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<RoleAdminRepository>();
        }

        public async Task<DataTableResponse<IEnumerable<RoleAdminGetDto>>> GetRolesInAdmin(int adminId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetByAdminId");

                var list =
                     await dbConnection
                    .QueryAsync<RoleAdminGetDto>(sql, new { AdminId = adminId }, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<RoleAdminGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<RoleAdminGetDto>>(errors);
                return result;
            }
        }

        public async Task<DataResponse<int>> UpdatAdminRolesAsync(RoleAdminUpdateCommand command)
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

        public async Task<DataTableResponse<IEnumerable<int>>> GetPermissionActionListByAdminId(int adminId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetPermissionListByAdminId");

                var list =
                     await dbConnection
                    .QueryAsync<int>(sql, new { AdminId = adminId }, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.Count();
                var result = new DataTableResponse<IEnumerable<int>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<int>>(errors);
                return result;
            }
        }

    }
}
