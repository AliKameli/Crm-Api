using CRCIS.Web.INoor.CRM.Contract.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCIS.Web.INoor.CRM.Data.Database
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly ISqlServerSettings _sqlServerSettings;

        public SqlConnectionFactory(ISqlServerSettings sqlServerSettings)
        {
            _sqlServerSettings = sqlServerSettings;
        }

        public IDbConnection GetOpenConnection()
        {
            return new SqlConnection(_sqlServerSettings.ConnectionString);
        }

        public string SpInstanceFree(string instanceName, string tableName, string action)
        {
            return $"[{instanceName}].[sp{tableName}{action}]";
        }
    }
}
