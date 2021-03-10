using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRCIS.Web.INoor.CRM.Data.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
        string SpInstanceFree(string instanceName, string tableName, string action);
    }
}
