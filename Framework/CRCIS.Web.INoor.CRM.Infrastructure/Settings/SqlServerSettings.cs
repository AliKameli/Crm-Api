using CRCIS.Web.INoor.CRM.Contract.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Settings
{
    public class SqlServerSettings: ISqlServerSettings
    {
        public string ConnectionString { get; set; }
    }
}
