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
        public string CommentNo { get; private set; }
        public string TypeOfComment { get; private set; }
        public string SnId { get; private set; }
        public string Sk { get; private set; }
        public string ActivationCode { get; private set; }
        public string Secret { get; private set; }

        public NoorLockReportPagingQuery(
            Guid? noorUserId, string commentNo, string typeOfComment,
            string snId, string sk, string activationCode, string secret,
            int pageIndex, int pageSize)
            : base(pageIndex, pageSize)
        {
            NoorUserId = noorUserId;
            CommentNo = commentNo;
            TypeOfComment = typeOfComment;
            SnId = snId;
            Sk = sk;
            ActivationCode = activationCode;
            Secret = secret;
        }

      
    }
}
