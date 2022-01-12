using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Commands;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Answers.CommonAnswer.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Answers
{
    public class CommonAnswerRepository : BaseRepository, ICommonAnswerRepository
    {
        protected override string TableName =>"CommonAnswer";
        private ILogger _logger;
        public CommonAnswerRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<CommonAnswerRepository>();
        }
        public async Task<DataResponse<IEnumerable<CommonAnswerGetDto>>> GetAsync(CommonAnswerDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<CommonAnswerGetDto>(sql, query, commandType: CommandType.StoredProcedure);
                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<CommonAnswerGetDto>>(list,totalCount);
                return result;
                    
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<CommonAnswerGetDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<CommonAnswerModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var commonAnswer =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<CommonAnswerModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (commonAnswer != null)
                    return new DataResponse<CommonAnswerModel>(commonAnswer);

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<CommonAnswerModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<CommonAnswerModel>(errors);
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
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<DropDownListDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(CommonAnswerCreateCommand command)
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
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> UpdateAsync(CommonAnswerUpdateCommand command)
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
                _logger.LogException(ex);

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
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<int>(errors);
                return result;
            }
        }
    }
}
