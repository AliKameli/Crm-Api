using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class RabbitImportCaseRepository : BaseRepository, IRabbitImportCaseRepository
    {
        public RabbitImportCaseRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
        }

        protected override string TableName => "ImportCase";

        public async Task<DataResponse<int>> CreateFromRabbiImporttAsync(RabbitImportCaseCreateCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                var sqlCreateId = _sqlConnectionFactory.SpInstanceFree("CRM", "Case", "Create");

                var id =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<long>(sqlCreateId, new { }, commandType: CommandType.StoredProcedure, transaction: transaction);

                command.Id = id;
                var sqlCreate = _sqlConnectionFactory.SpInstanceFree("CRM", "ImportCaseRabbit", "Create");

                await dbConnection
                     .QueryFirstOrDefaultAsync<long>(sqlCreate, command, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    null, command.Id, DateTime.Now,
                    2//ثبت اتوماتیک سیستم
                    );
                var caseHistoryId =
                                  await dbConnection
                                 .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();

                return new DataResponse<int>(true);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
    }
}
