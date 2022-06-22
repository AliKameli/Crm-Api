using CRCIS.Web.INoor.CRM.Contract.Repositories.ProductSubject;
using CRCIS.Web.INoor.CRM.Data.Database;
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

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.ProductSubject
{
    public class ProductSubjectRepository : BaseRepository, IProductSubjectRepository
    {
        private readonly ILogger<ProductSubjectRepository> _logger;

        protected override string TableName => "ProductSubject";
        public ProductSubjectRepository(ISqlConnectionFactory sqlConnectionFactory, ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<ProductSubjectRepository>();
        }

        public async Task<DataResponse<int>> UpdateAsync(int subjectId)
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();
                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "Update");

                var id = await dbConnection.QueryAsync<int>(sql, new { SubjectId = subjectId }, commandType: CommandType.StoredProcedure);
                var result = new DataResponse<int>(id.FirstOrDefault());
                return result;

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
