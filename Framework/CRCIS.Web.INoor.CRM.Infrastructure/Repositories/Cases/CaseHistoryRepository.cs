using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Queries;
using CRCIS.Web.INoor.CRM.Infrastructure.Specifications.Case;
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
    public class CaseHistoryRepository : BaseRepository, ICaseHistoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        protected override string TableName => "";

        public CaseHistoryRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory,IMapper mapper) : base(sqlConnectionFactory)
        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CaseHistoryRepository>();
        }
        public async Task<DataResponse<CaseHistoriesFullDto>> CasePendingHistoryReportByCaseIdAsync(CasePendingHistoryReportByCaseIdQuery query)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", "", "CasePendingHistoryReportByCaseId");
                var historiesQuery =
                     await dbConnection
                    .QueryAsync<CasePendingHistoryReportByCaseIdDto>(sql, query, commandType: CommandType.StoredProcedure);

                var dto = historiesQuery.AsQueryable()
                    .ProjectTo<CaseHistoriesDto>(_mapper.ConfigurationProvider).AsEnumerable();

                var dtoFull = dto.PairCaseHistoriesFullDto();

                var result = new DataResponse<CaseHistoriesFullDto>(dtoFull);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var errors = new List<string> { "خطایی در دریافت تاریخچه از بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<CaseHistoriesFullDto>(errors);
                return result;
            }
        }
    }
}
