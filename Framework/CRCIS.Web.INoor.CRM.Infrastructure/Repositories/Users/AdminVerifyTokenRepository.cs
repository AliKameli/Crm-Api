using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Users
{
    public class AdminVerifyTokenRepository : BaseRepository, IAdminVerifyTokenRepository
    {
        private readonly ILogger _logger;
        protected override string TableName => "AdminCrmVerifyToken";
        public AdminVerifyTokenRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<AdminVerifyTokenRepository>();
        }

        public async Task<DataResponse<Guid>> CreateTokenAsync(int adminId, Dictionary<string, string> queryString, string action = "")
        {
            if (queryString == null)
            {
                queryString = new Dictionary<string, string>();
            }
            if (action == null)
            {
                action = "";
            }

            try
            {
                var command = new
                {
                    Id = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    ExpireAt = DateTime.Now.AddMinutes(2),
                    AdminId = adminId,
                    Action = action,
                    JsonData = System.Text.Json.JsonSerializer.Serialize(queryString)
                };

                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Create");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<Guid>(command.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<Guid>(errors);
                return result;
            }
        }

        public async Task<DataResponse<AdminByVerifyTokenDto>> GetAdminByVerifyToken(Guid verifyId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetAdminById");
                var command = new { Id = verifyId };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminByVerifyTokenDto>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminByVerifyTokenDto>(adminModel);

                var errors = new List<string> { "اذمین یافت نشد" };
                var result = new DataResponse<AdminByVerifyTokenDto>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminByVerifyTokenDto>(errors);
                return result;
            }
        }

        public async Task<DataResponse<AdminByVerifyTokenDto>> GetAdminByInoorIdAsync(Guid inoorId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetAdminById");
                var command = new { InoorId = inoorId };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminByVerifyTokenDto>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminByVerifyTokenDto>(adminModel);

                var errors = new List<string> { "اذمین یافت نشد" };
                var result = new DataResponse<AdminByVerifyTokenDto>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminByVerifyTokenDto>(errors);
                return result;
            }
        }

        public async Task<DataResponse<Guid>> DeleteVerifyToken(Guid verifyId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
                var command = new { @VerifyId = verifyId };

                await dbConnection
               .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<Guid>(verifyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<Guid>(errors);
                return result;
            }
        }
    }
}