﻿using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class ImportCaseRepository : BaseRepository, IImportCaseRepository
    {
        private readonly IIdentity _identity;
        protected override string TableName => "ImportCase";
        public ImportCaseRepository(ISqlConnectionFactory sqlConnectionFactory, IIdentity identity) : base(sqlConnectionFactory)
        {
            _identity = identity;
        }
        public async Task<DataTableResponse<IEnumerable<ImportCaseGetDto>>> GetAsync(ImportCaseDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<ImportCaseGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<ImportCaseGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ImportCaseGetDto>>(errors);
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
                //_logger.LogError(ex.Message);

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
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }

        public async Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {
            var AdminId = _identity.GetAdminId();
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
                    AdminId, command.CaseId, DateTime.Now,
                        5//	انتقال به کارتابل خودم
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

        public async Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveCommand command)
        {
            var AdminId = _identity.GetAdminId();
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
                    AdminId, command.CaseId, DateTime.Now,
                        8//	آرشیو مستقیم
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