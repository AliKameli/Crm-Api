using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Reports.NoorLock.Queries
{
    public class NoorLockReportRowNumberQuery
    {
        public Guid? NoorUserId { get; private set; }
        public string AppHashKey { get; private set; }
        public string ProductSecret { get; private set; }


        public long RowNumber { get; private set; }

        public NoorLockReportRowNumberQuery(Guid? noorUserId, string appHashKey, string productSecret, long rowNumber)
        {
            NoorUserId = noorUserId;
            ProductSecret = productSecret;
            RowNumber = rowNumber;
            AppHashKey = appHashKey;
        }
    }
}
