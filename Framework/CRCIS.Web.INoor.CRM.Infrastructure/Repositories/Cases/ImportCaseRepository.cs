using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Queries;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Case;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using CRCIS.Web.INoor.CRM.Utility.Enums.Extensions;
using Microsoft.Extensions.Logging;
using CRCIS.Web.INoor.CRM.Utility.Extensions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class ImportCaseRepository : BaseRepository, IImportCaseRepository
    {
        private readonly IIdentity _identity;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        protected override string TableName => "ImportCase";
        public ImportCaseRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory,
            IIdentity identity, IMapper mapper) : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<ImportCaseRepository>();
            _identity = identity;
            _mapper = mapper;
        }
        public async Task<DataTableResponse<IEnumerable<ImportCaseGetFullDto>>> GetAsync(ImportCaseDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<ImportCaseGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list.AsQueryable().ProjectTo<ImportCaseGetFullDto>(_mapper.ConfigurationProvider).ToList();

                var result = new DataTableResponse<IEnumerable<ImportCaseGetFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ImportCaseGetFullDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(ImportCaseCreateCommand command, List<int> caseSubjectIds)
        {
            var AdminId = _identity.GetAdminId();
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
                var sqlCreate = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Create");

                await dbConnection
                     .QueryFirstOrDefaultAsync<long>(sqlCreate, command, commandType: CommandType.StoredProcedure, transaction: transaction);

                if (caseSubjectIds == null)
                {
                    caseSubjectIds = new List<int>();
                }

                var commandSubjects = new
                {
                    CaseSubjectIds = string.Join(',', caseSubjectIds),
                    CaseId = id,
                    CreateAt = DateTime.Now
                };
                var sqlSubjects = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseSubject", "Create");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sqlSubjects, commandSubjects, commandType: CommandType.StoredProcedure, transaction: transaction);

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    AdminId, command.Id, DateTime.Now,
                    1//ثبت دستی ادمین
                    );
                var caseHistoryId =
                                  await dbConnection
                                 .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();

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

        public async Task<DataResponse<int>> CreateAndMoveToAdmin(ImportCaseCreateCommand command, int adminId, List<int> caseSubjectIds)
        {
            var AdminId = _identity.GetAdminId();
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
                var sqlCreate = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Create");

                await dbConnection
               .QueryFirstOrDefaultAsync<long>(sqlCreate, command, commandType: CommandType.StoredProcedure, transaction: transaction);


                var commandSubjects = new
                {
                    CaseSubjectIds = string.Join(',', caseSubjectIds),
                    CaseId = id,
                    CreateAt = DateTime.Now
                };
                var sqlSubjects = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseSubject", "Create");

                var execute =
                     await dbConnection
                    .ExecuteAsync(sqlSubjects, commandSubjects, commandType: CommandType.StoredProcedure, transaction: transaction);


                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    AdminId, command.Id, DateTime.Now,
                    1//ثبت دستی ادمین
                    );
                var caseHistoryId =
                                  await dbConnection
                                 .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);


                var sqlMove = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToAdmin");
                var commandMove = new MoveCaseToCurrentAdminCardboardCommand(adminId, id);
                var executeMove =
                     await dbConnection
                    .ExecuteAsync(sqlMove, commandMove, commandType: CommandType.StoredProcedure, transaction: transaction);


                var sqlDelete = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
                var commandDelete = new { Id = id };

                await dbConnection
                    .QueryFirstOrDefaultAsync(sqlDelete, commandDelete, commandType: CommandType.StoredProcedure, transaction: transaction);


                sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                commandCaseHistory = new CaseHistoryCreateCommand(
                   AdminId, command.Id, DateTime.Now,
                       5//انتقال به کارتابل خودم
                   );
                caseHistoryId =
                              await dbConnection
                             .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();

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

        public async Task MoveCaseToAdminAsync(long caseId)
        {
            var adminId = _identity.GetAdminId();
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
            var sqlMove = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToAdmin");
            var commandMove = new MoveCaseToCurrentAdminCardboardCommand(adminId, caseId);
            var executeMove =
                 await dbConnection
                .ExecuteAsync(sqlMove, commandMove, commandType: CommandType.StoredProcedure);
        }
        public async Task DeleteCaseAsync(long caseId)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

            var sqlDelete = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
            var commandDelete = new { Id = caseId };
            await dbConnection
                .ExecuteAsync(sqlDelete, commandDelete, commandType: CommandType.StoredProcedure);
        }

        public async Task<DataResponse<int>> AddCaseHistoryMoveCaseToCurrentAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {
            var AdminId = _identity.GetAdminId();

            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

            var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
            var commandCaseHistory = new CaseHistoryCreateCommand(
                AdminId, command.CaseId, DateTime.Now,
                    OperationType.MoveToMyCartable.ToInt32()//	5 = انتقال به کارتابل خودم
                );
            var caseHistoryId =
               await dbConnection
              .ExecuteScalarAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure);

            return new DataResponse<int>(true);
        }


        public async Task MoveCaseToArchiveAsync(long caseId)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
            var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToArchive");
            var command = new MoveCaseToArchiveCommand(caseId);
            var execute =
                 await dbConnection
                .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);

        }

        public async Task<DataResponse<int>> AddCaseHistoryMoveCaseToArchiveAsync(MoveCaseToArchiveCommand command)
        {
            var AdminId = _identity.GetAdminId();
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
            dbConnection.Open();

            var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
            var commandCaseHistory = new CaseHistoryCreateCommand(
                AdminId, command.CaseId, DateTime.Now,
                   OperationType.DirectArchive.ToInt32()//	8 = آرشیو مستقیم
                );
            var caseHistoryId =
               await dbConnection
              .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure);

            return new DataResponse<int>(true);

        }
    }
}