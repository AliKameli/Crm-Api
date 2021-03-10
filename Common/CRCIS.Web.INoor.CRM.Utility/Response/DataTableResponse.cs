using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Response
{
    public class DataTableResponse<TData> : DataResponse<TData>
    {
        public long TotalCount { get; private set; }

        public DataTableResponse(bool success)
            : base(success)
        {
        }

        public DataTableResponse(TData data, long totalCount)
            : base(data)
        {
            TotalCount = totalCount;
        }

        public DataTableResponse(IList<string> errors)
            : base(errors)
        {
        }

        public DataTableResponse(bool success, IList<string> errors, TData data, long totalCount)
            : base(success, errors, data)
        {
            TotalCount = totalCount;
        }

    }
}
