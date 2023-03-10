using CRCIS.Web.INoor.CRM.Utility.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries
{
    public class SubjectChildrenDataTableQuery : AbstractDataTableQuery
    {
        public int? ParentId { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }

        public SubjectChildrenDataTableQuery(int pageIndex, int pageSize, int? parentId, string title, string code)
            : base(pageIndex, pageSize)
        {
            ParentId = parentId;
            Title = title?.Trim();
            Code = code?.Trim();
        }
    }
}
