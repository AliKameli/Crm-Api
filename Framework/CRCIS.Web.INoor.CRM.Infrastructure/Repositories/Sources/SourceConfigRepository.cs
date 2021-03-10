using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Sources
{
    public class SourceConfigRepository : BaseRepository, ISourceConfigRepository
    {
        protected override string TableName => "SourceConfig";
        public SourceConfigRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
        }


        public async Task<DataResponse<IEnumerable<SourceConfigGetDto>>> GetAsync(SourceConfigDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<SourceConfigGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<SourceConfigGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<SourceConfigGetDto>>(errors);
                return result;
            }

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
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<SourceConfigModel>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(SourceConfigCreateCommand command)
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
        public async Task<DataResponse<int>> UpdateAsync(SourceConfigUpdateCommand command)
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
    }
}