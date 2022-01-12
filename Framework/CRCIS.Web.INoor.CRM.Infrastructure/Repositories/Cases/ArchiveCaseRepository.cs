using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Queris;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Enums;
using CRCIS.Web.INoor.CRM.Utility.Enums.Extensions;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CRCIS.Web.INoor.CRM.Utility.Extensions;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases
{
    public class ArchiveCaseRepository : BaseRepository, IArchiveCaseRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        protected override string TableName => "ArchiveCase";
        public ArchiveCaseRepository(ISqlConnectionFactory sqlConnectionFactory, 
            ILoggerFactory loggerFactory,
            IMapper mapper) : base(sqlConnectionFactory)
        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<ArchiveCaseRepository>();
        }

        public async Task<DataTableResponse<IEnumerable<ArchiveCaseGetFullDto>>> GetAsync(ArchiveCaseDataTableQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var list =
                     await dbConnection
                    .QueryAsync<ArchiveCaseGetDto>(sql, query, commandType: CommandType.StoredProcedure);

                long totalCount = (list == null || !list.Any()) ? 0 : list.FirstOrDefault().TotalCount;

                var listFull = list.AsQueryable().ProjectTo<ArchiveCaseGetFullDto>(_mapper.ConfigurationProvider).ToList();

                var result = new DataTableResponse<IEnumerable<ArchiveCaseGetFullDto>>(listFull, totalCount);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataTableResponse<IEnumerable<ArchiveCaseGetFullDto>>(errors);
                return result;
            }
        }

        public async Task MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
            var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "MoveToAdmin");
            var execute =
                 await dbConnection
                .ExecuteAsync(sql, command, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(long caseId)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
            var sqlDelete = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Delete");
            var commandDelete = new { Id = caseId };

            await dbConnection
                .QueryFirstOrDefaultAsync(sqlDelete, commandDelete, commandType: CommandType.StoredProcedure);

        }

        public async Task AddCaseHistoryMoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command)
        {
            using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

            var sqlCaseHistory = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "Create");
            var commandCaseHistory = new CaseHistoryCreateCommand(
                command.AdminId, command.CaseId, DateTime.Now,
                OperationType.BackFromArchiveToCurrnetAdminCartable.ToInt32()// 7	بازگشت از آرشیو به کارتابل خودم
                );
            var caseHistoryId =
                       await dbConnection
                      .QueryFirstOrDefaultAsync<long>(sqlCaseHistory, commandCaseHistory, commandType: CommandType.StoredProcedure);

        }

    }
}
