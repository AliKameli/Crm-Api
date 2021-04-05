using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseHistory.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
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
        protected override string TableName => throw new NotImplementedException();
        public CaseHistoryRepository(ISqlConnectionFactory sqlConnectionFactory, IMapper mapper) : base(sqlConnectionFactory)
        {
            _mapper = mapper;
        }
        public async Task<DataResponse<IEnumerable<CaseHistoriesDto>>> GetReportForCaseAsync(long id)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                dbConnection.Open();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", "CaseHistory", "ReportByCaseId");
                var query = new { CaseId = id };
                var historiesQuery =
                     await dbConnection
                    .QueryAsync<CaseHistoriesQuery>(sql, query, commandType: CommandType.StoredProcedure);

                var dto = historiesQuery.AsQueryable()
                    .ProjectTo<CaseHistoriesDto>(_mapper.ConfigurationProvider).AsEnumerable();

                var result = new DataResponse<IEnumerable<CaseHistoriesDto>>(dto);

                return result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                var errors = new List<string> { "خطایی در دریافت تاریخچه از بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<IEnumerable<CaseHistoriesDto>>(errors);
                return result;
            }
        }
    }
}
