using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries
{
    public class NoorLockReportPagingQuery : AbstractDataTableQuery
    { 
        public Guid? NoorUserId { get; private set; }
        public string AppHashKey { get; private set; }
        public string ProductSecret { get; private set; }

        public NoorLockReportPagingQuery(
            string appHashKey  , string productSecret,
            int pageIndex, int pageSize)
            : base(pageIndex, pageSize)
        {
            NoorUserId = null;
            AppHashKey = appHashKey;
            ProductSecret = productSecret;
        }
        public NoorLockReportPagingQuery(
            Guid noorUserId, string productSecret,
            int pageIndex, int pageSize)
            : base(pageIndex, pageSize)
        {
            NoorUserId = noorUserId;
            AppHashKey = null;
            ProductSecret = productSecret;
        }

      
    }
}
