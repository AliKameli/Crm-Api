using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
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
        protected override string TableName => "AdminCrmVerifyToken";
        public AdminVerifyTokenRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
        }

        public async Task<DataResponse<Guid>> CreateTokenAsync(int adminId)
        {
            try
            {
                var command = new
                {
                    Id = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    ExpireAt = DateTime.Now.AddMinutes(2),
                    AdminId = adminId,
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
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<Guid>(errors);
                return result;
            }
        }

        public async Task<DataResponse<AdminModel>> GetAdminByVerifyToken(Guid verifyId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetAdminById");
                var command = new { Id = verifyId };
                var adminModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AdminModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (adminModel != null)
                    return new DataResponse<AdminModel>(adminModel);

                var errors = new List<string> { "اذمین یافت نشد" };
                var result = new DataResponse<AdminModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AdminModel>(errors);
                return result;
            }
        }
    }

}
