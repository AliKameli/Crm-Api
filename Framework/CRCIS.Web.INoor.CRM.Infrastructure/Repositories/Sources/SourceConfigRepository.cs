using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Sources
{
    public class SourceConfigRepository : BaseRepository, ISourceConfigRepository
    {
        protected override string TableName => "SourceConfig";
        private readonly ILogger _logger;
        public SourceConfigRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<SourceConfigRepository>();
        }

        public async Task<DataResponse<SourceConfigModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var caseStatus =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<SourceConfigModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (caseStatus != null)
                    return new DataResponse<SourceConfigModel>(caseStatus);

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<SourceConfigModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<SourceConfigModel>(errors);
                return result;
            }
        }

        public async Task<DataResponse<IEnumerable<SourceConfigModel>>> GetBySourceTypesIdAsync(int sourceTypeId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetBySourceTypeId");
                var command = new { SourceTypeId = sourceTypeId };
                var sourceConfigModels =
                     await dbConnection
                    .QueryAsync<SourceConfigModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (sourceConfigModels != null)
                    return new DataResponse<IEnumerable<SourceConfigModel>>(sourceConfigModels);

                var errors = new List<string> { "منبع تنظیمات یافت نشد" };
                var result = new DataResponse<IEnumerable<SourceConfigModel>>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<SourceConfigModel>>(errors);
                return result;
            }
        }

        public async Task<DataResponse<IEnumerable<SourceConfigModel>>> GetByAnswerMethodIdAsync(int answerMethodId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetByAnswerMethodId");
                var command = new { AnswerMethodId = answerMethodId };
                var sourceConfigModels =
                     await dbConnection
                    .QueryAsync<SourceConfigModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (sourceConfigModels != null)
                    return new DataResponse<IEnumerable<SourceConfigModel>>(sourceConfigModels);

                var errors = new List<string> { "منبع تنظیمات یافت نشد" };
                var result = new DataResponse<IEnumerable<SourceConfigModel>>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<SourceConfigModel>>(errors);
                return result;
            }
        }


    }
}