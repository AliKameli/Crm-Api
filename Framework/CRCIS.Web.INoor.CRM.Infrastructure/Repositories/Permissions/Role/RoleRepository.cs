using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Commands;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Queris;
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
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        protected override string TableName => "Role";
        private readonly ILogger _logger;
        public RoleRepository(ILoggerFactory loggerFactory, ISqlConnectionFactory sqlConnectionFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<RoleRepository>();
        }

        public async Task<DataTableResponse<IEnumerable<RoleGetDto>>> GetAsync(RoleDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<RoleGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<RoleGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<RoleGetDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(RoleCreateCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Create");

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
        public async Task<DataResponse<int>> UpdateAsync(RoleUpdateCommand command)
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

        public async Task<DataTableResponse<IEnumerable<RoleGetShowTreeDto>>> GetShowTreeAsync() {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "ShowTreeList");

                var list =
                     await dbConnection
                    .QueryAsync<RoleGetShowTreeDto>(sql, new { }, commandType: CommandType.StoredProcedure);

                var totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<RoleGetShowTreeDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<RoleGetShowTreeDto>>(errors);
                return result;
            }
        }


    }
}
