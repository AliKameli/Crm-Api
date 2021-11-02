using AutoMapper;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Case;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class PendingCaseRepository : BaseRepository, IPendingCaseRepository
    {
        private readonly IMapper _mapper;
        private readonly IIdentity _identity;
        protected override string TableName => "PendingCase";
        public PendingCaseRepository(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper, IIdentity identity) : base(sqlConnectionFactory)
        {
            _mapper = mapper;
            _identity = identity;
        }
        public async Task<DataTableResponse<IEnumerable<PendingCaseGetDto>>> GetForAdminAsync(AdminPendingCaseDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "AdminList");

                var list =
                     await dbConnection
                    .QueryAsync<PendingCaseGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<PendingCaseGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<PendingCaseGetDto>>(errors);
                return result;
            }
        }

        public async Task<DataResponse<PendingCaseFullDto>> GetByIdAsync(long id)
        {
            var adminId = _identity.GetAdminId();
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var pendingCaseModel =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<PendingCaseModel>(sql, command, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    adminId, command.Id, DateTime.Now,
                    3//	مشاهده جزییات مورد
                    );
                var caseHistoryId =
                                  await dbConnection
                                 .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                var commandSeen = new { CaseId = command.Id };
                var sqlCaseSeen = _sqlConnectionFactory.SpInstanceFree("CRM", "PendingCase", "UpdateSeen");
                await dbConnection
                    .QueryFirstOrDefaultAsync<long>(sqlCaseSeen, commandSeen, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();

                if (pendingCaseModel != null)
                {
                    var dto = _mapper.Map<PendingCaseFullDto>(pendingCaseModel);
                    dto = dto.PairSuggestionsForAnswer();
                    return new DataResponse<PendingCaseFullDto>(dto);
                }

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<PendingCaseFullDto>(errors);

                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<PendingCaseFullDto>(errors);
                return result;
            }
        }

        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardCommand command)
        {
            var adminId = _identity.GetAdminId();
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToAdmin");
                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    adminId, command.CaseId, DateTime.Now,
                    6//	انتقال به کارتابل همکار
                    );
                var caseHistoryId =
                          await dbConnection
                         .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                commandCaseHistory = new CaseHistoryCreateCommand(
                   command.AdminId, command.CaseId, DateTime.Now,
                   10//	انتصاب همکار پاسخگو
                   );
                caseHistoryId =
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
        public async Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveCommand command)
        {
            var adminId = _identity.GetAdminId();
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToArchive");
                var execute =
                     await dbConnection
                    .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlDelete = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
                var commandDelete = new { Id = command.CaseId };

                await dbConnection
                    .QueryFirstOrDefaultAsync(sqlDelete, commandDelete, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    adminId, command.CaseId, DateTime.Now,
                    9//آرشیو از کارتابل
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
