using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Sources
{
    public class SourceTypeRepository : BaseRepository, ISourceTypeRepository
    {
        protected override string TableName => "SourceType";
        public SourceTypeRepository(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
        {
        }
        
        public async Task<DataTableResponse<IEnumerable<SourceTypeGetDto>>> GetAsync(SourceTypeDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<SourceTypeGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<SourceTypeGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<SourceTypeGetDto>>(errors);
                return result;
            }
        }
        public async Task<DataResponse<SourceTypeModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var answerMethod =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<SourceTypeModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (answerMethod != null)
                    return new DataResponse<SourceTypeModel>(answerMethod);

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<SourceTypeModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<SourceTypeModel>(errors);
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

        public async Task<DataResponse<int>> CreateAsync(SourceTypeCreateCommand command)
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
        public async Task<DataResponse<int>> UpdateAsync(SourceTypeUpdateCommand command)
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
