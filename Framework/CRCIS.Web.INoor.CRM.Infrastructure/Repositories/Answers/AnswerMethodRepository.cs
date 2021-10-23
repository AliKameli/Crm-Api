using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Answers.AnswerMethod.Queris;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
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
    public class AnswerMethodRepository : BaseRepository, IAnswerMethodRepository
    {
        protected override string TableName => "AnswerMethod";
        private ILogger _logger;
        public AnswerMethodRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory) 
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<AnswerMethodRepository>();
        }

        public async Task<DataResponse<IEnumerable<AnswerMethodGetDto>>> GetAsync(AnswerMethodDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<AnswerMethodGetDto>(sql, query, commandType: CommandType.StoredProcedure);
                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<AnswerMethodGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<AnswerMethodGetDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<AnswerMethodModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var answerMethod =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<AnswerMethodModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (answerMethod != null)
                    return new DataResponse<AnswerMethodModel>(answerMethod);

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<AnswerMethodModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<AnswerMethodModel>(errors);
                return result;
            }
        }
        public async Task<DataResponse<IEnumerable<DropDownListDto>>> GetDropDownListAsync()
        {

            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "DropDownList");

                var list =
                     await dbConnection
                    .QueryAsync<DropDownListDto>(sql, commandType: CommandType.StoredProcedure);


                var result = new DataResponse<IEnumerable<DropDownListDto>>(list);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<DropDownListDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(AnswerMethodCreateCommand command)
        {
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
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> UpdateAsync(AnswerMethodUpdateCommand command)
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
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> DeleteAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
                var command = new { Id = id };

                await dbConnection
                    .QueryFirstOrDefaultAsync(sql, command, commandType: CommandType.StoredProcedure);

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
