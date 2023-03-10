using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.Answering.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingHistory.Commands;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Answers
{
    public class PendingHistoryRepository : BaseRepository, IPendingHistoryRepository
    {
        protected override string TableName => "CaseHistory";
        private ILogger _logger;
        public PendingHistoryRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<PendingHistoryRepository>();
        }

        public async Task<DataResponse<long>> CreateAsync(AnsweringCreateDto dto)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                using var transaction = dbConnection.BeginTransaction();

                var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
                var commandCaseHistory = new CaseHistoryCreateCommand(
                    dto.AdminId, dto.CaseId, DateTime.Now,
                    4 //پاسخ دهی ادمین
                    );
                var caseHistoryId =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                var commandPendingHistory = new PendingHistoryCreateCommand(caseHistoryId,
                    dto.AnswerMethodId,
                    dto.AnswerText,
                    dto.AnswerSource,
                    dto.Attachments == null ? null : System.Text.Json.JsonSerializer.Serialize(dto.Attachments));

                var sqlPendingHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "PendingHistory", "Create");
                var pendingHistoryId = await dbConnection
                  .QueryFirstOrDefaultAsync<long>(sqlPendingHistory, commandPendingHistory, commandType: CommandType.StoredProcedure, transaction: transaction);

                var commandCaseAnswer = new { CaseId = dto.CaseId };
                var sqlCaseAnswer = _sqlConnectionFactory.SpInstanceFree("CRM", "PendingCase", "UpdateAnswer");
                await dbConnection
                    .QueryFirstOrDefaultAsync<long>(sqlCaseAnswer, commandCaseAnswer, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();

                return new DataResponse<long>(pendingHistoryId);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<long>(errors);
                return result;
            }
        }

        public async Task<DataResponse<int>> UpdateResulAsync(AnsweringUpdateResultCommand command)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                var sqlPendingHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "PendingHistory", "UpdateResult");
                await dbConnection
                           .QueryFirstOrDefaultAsync<long>(sqlPendingHistory, command, commandType: CommandType.StoredProcedure);
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
    }
}
