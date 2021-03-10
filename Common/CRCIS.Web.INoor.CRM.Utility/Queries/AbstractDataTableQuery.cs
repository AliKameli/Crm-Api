using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Utility.Queries
{
    public abstract class AbstractDataTableQuery
    {
        public int PageIndex { get; protected set; }
        public int PageSize { get; protected set; }
      


        public AbstractDataTableQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
           
        }
    }
}
