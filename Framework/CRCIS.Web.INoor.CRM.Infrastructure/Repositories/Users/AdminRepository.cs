using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using CRCIS.Web.INoor.CRM.Data.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CRCIS.Web.INoor.CRM.Utility.Response;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Queries;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin.Commands;
using CRCIS.Web.INoor.CRM.Contract.Security;
using Microsoft.Extensions.Logging;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Users
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        private readonly ISecurityService _securityService;
        protected override string TableName => "Admin";
        private ILogger _logger;
        public AdminRepository(ISqlConnectionFactory sqlConnectionFactory, ISecurityService securityService,
            ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _securityService = securityService;
            _logger = loggerFactory.CreateLogger<AdminRepository>();
        }
        public async Task<DataTableResponse<IEnumerable<AdminGetDto>>> GetAsync(AdminDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<AdminGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<AdminGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<AdminGetDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<AdminGetDto>> GetProfileByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "ProfileGetById");
                var command = new { Id = id };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminGetDto>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminGetDto>(adminModel);

                var errors = new List<string> { "ادمین یافت نشد" };
                var result = new DataResponse<AdminGetDto>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminGetDto>(errors);
                return result;
            }
        }
        public async Task<DataResponse<AdminModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminModel>(adminModel);

                var errors = new List<string> { "ادمین یافت نشد" };
                var result = new DataResponse<AdminModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminModel>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> UpdatePasswordHashAsync(int adminId, string newPasswordHash)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "UpdatePassword");
                var command = new
                {
                    Id = adminId,
                    PasswordHash = newPasswordHash
                };
                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

                return new DataResponse<int>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<IEnumerable<AdminDropDownListDto>>> GetDropDownListAsync()
        {

            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "DropDownList");

                var list =
                     await dbConnection
                    .QueryAsync<AdminDropDownListDto>(sql, commandType: CommandType.StoredProcedure);


                var result = new DataResponse<IEnumerable<AdminDropDownListDto>>(list);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<AdminDropDownListDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(AdminCreateCommand command)
        {
            command = new AdminCreateCommand(
                 command.Username,
                 _securityService.GetSha256Hash(command.PasswordHash),
                 command.Name,
                 command.Family,
                 command.Mobile,
                 command.NoorPersonId);
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
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> UpdateAsync(AdminUpdateCommand command)
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
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }

        public async Task<DataResponse<AdminModel>> FindAdminAsync(AdminLoginQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetByUserAndPasswordHash");
                var command = new
                {
                    Username = query.Username,
                    PasswordHash = _securityService.GetSha256Hash(query.Password)
                };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminModel>(adminModel);

                var errors = new List<string> { "ادمین یافت نشد" };
                var result = new DataResponse<AdminModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminModel>(errors);
                return result;
            }
        }

        public async Task<DataResponse<AdminModel>> FindAdminAsync(int adminId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetDetailsById");
                var command = new { Id = adminId };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminModel>(adminModel);

                var errors = new List<string> { "ادمین یافت نشد" };
                var result = new DataResponse<AdminModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminModel>(errors);
                return result;
            }
        }

        public async Task<DataResponse<AdminModel>> FindAdminAsync(Guid personId)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
            //var dd = dbConnection.QuerySingleOrDefault("EXEC [CRM].[spAdminGetByPersonId] @personId", new { personId }, commandType: CommandType.Text);
            //return await Task.FromResult(new DataResponse<AdminModel>(new List<string>() { dd.ToString() }));

            var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetByPersonId");
            var command = new { PersonId = personId };
            var adminModel =
                 await dbConnection
                .QueryFirstOrDefaultAsync<AdminModel>(sql, command, commandType: CommandType.StoredProcedure);

            var result = new DataResponse<AdminModel>(adminModel);
            return result;

        }
        public async Task UpdateAdminLastActivityDateAsync(int adminId)
        {
            // throw new NotImplementedException();
        }
    }
}
