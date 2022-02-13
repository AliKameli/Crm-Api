using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Menu;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Domain.Permissions.Action.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.Menu
{
    public class ActionRepository : BaseRepository, IActionRepository
    {
        private readonly ILogger _logger;
        protected override string TableName => "Action";
        public ActionRepository(ISqlConnectionFactory sqlConnectionFactory,ILoggerFactory loggerFactory)
            : base(sqlConnectionFactory)
        {
            _logger = loggerFactory.CreateLogger<ActionRepository>();
        }

        public async Task<DataResponse<ActionDto>> GetAsync()
        {
            try
            {
                using var dbConnection = _sqlConnectionFactory.GetOpenConnection();

                var sql = _sqlConnectionFactory.SpInstanceFree("CRM", TableName, "List");

                var dto =
                     await dbConnection
                    .QueryFirstAsync<ActionDto>(sql, new { }, commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(dto?.ResultJsonPath) == false)
                {
                    dto.ResultJsonPath =
                        dto
                         ?.ResultJsonPath
                         ?.Replace("Menus_CTE1", "children")
                          .Replace("Menus_CTE2", "children")
                          .Replace("Menus_CTE3", "children")
                          .Replace("Menus_CTE4", "children")
                          .Replace("[{}]", "[]")
                          .Replace("[]", "[]")
                          //.Replace("\"children\":[]", "")
                        ;
                }
                var result = new DataResponse<ActionDto>(dto);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var errors = new List<string> { "خطایی در ارتباط با بانک اطلاعاتی رخ داده است" };
                var result = new DataResponse<ActionDto>(errors);
                return result;
            }
        }

    }
}
