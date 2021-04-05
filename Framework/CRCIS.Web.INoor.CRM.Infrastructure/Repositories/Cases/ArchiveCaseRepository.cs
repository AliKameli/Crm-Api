using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Queris;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
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
    public class ArchiveCaseRepository : BaseRepository, IArchiveCaseRepository
    {
        protected override string TableName => "ArchiveCase";
        public ArchiveCaseRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
        }
        public async Task<DataTableResponse<IEnumerable<ArchiveCaseGetDto>>> GetAsync(ArchiveCaseDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<ArchiveCaseGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<ArchiveCaseGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ArchiveCaseGetDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToAdmin");
                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure, transaction: transaction);



                var sqlDelete = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
                var commandDelete = new { Id = command.CaseId };

                await dbConnection
                    .QueryFirstOrDefaultAsync(sqlDelete, commandDelete, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    command.AdminId, command.CaseId, DateTime.Now,
                    7//	بازگشت از آرشیو به کارتابل خودم
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
