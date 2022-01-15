using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Queries;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class CaseSubjectRepository : BaseRepository, ICaseSubjectRepository
    {
        protected override string TableName => "CaseSubject";
        private readonly ILogger _logger;
        public CaseSubjectRepository(ISqlConnectionFactory sqlConnectionFactory,ILoggerFactory loggerFactory) : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<CaseSubjectRepository>();
        }
        public async Task<DataTableResponse<IEnumerable<CaseSubjectGetDto>>> GetAsync(CaseSubjectDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<CaseSubjectGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                int totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;
                var result = new DataTableResponse<IEnumerable<CaseSubjectGetDto>>(list, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<CaseSubjectGetDto>>(errors);
                return result;
            }
        }

        public async Task<DataResponse<CaseSubjectModel>> GetByIdAsync(int id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetById");
                var command = new { Id = id };
                var caseSubject =
                     await dbConnection
                    .QueryFirstOrDefaultAsync<CaseSubjectModel>(sql, command, commandType: CommandType.StoredProcedure);

                if (caseSubject != null)
                    return new DataResponse<CaseSubjectModel>(caseSubject);

                var errors = new List<string> { "وضعیت مورد یافت نشد" };
                var result = new DataResponse<CaseSubjectModel>(errors);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<CaseSubjectModel>(errors);
                return result;
            }
        }
        public async Task<DataResponse<int>> CreateAsync(CaseSubjectCreateCommand command)
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
        public async Task<DataResponse<int>> UpdateAsync(CaseSubjectUpdateCommand command)
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


        public async Task<IEnumerable<CaseSubjectFullDto>> GetCaseSubjectsByCaseIdAsync(long caseId)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

            var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "GetByCaseId");
            var command = new { CaseId = caseId };
            var caseSubjects =
                   await dbConnection
                  .QueryAsync<CaseSubjectFullDto>(sql, command, commandType: CommandType.StoredProcedure);
            return caseSubjects;

        }
        public async Task UpdateCaseAddSubjectAsync(UpdateCaseAddSubjectCommand command)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

            var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "AddSubject");

            var execute =
                 await dbConnection
                .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);
        }
        public async Task UpdateCaseRemoveSubjectAsync(UpdateCaseRemoveSubjectCommand command)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

            var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "RemoveSubject");

            var execute =
                 await dbConnection
                .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);
        }
    }
}
