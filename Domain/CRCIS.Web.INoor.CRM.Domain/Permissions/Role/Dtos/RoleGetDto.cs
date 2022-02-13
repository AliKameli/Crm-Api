using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Dtos
{
    public class RoleGetDto
    {
        public int Id { get; set; }
        public long RowNumber { get; set; }
        public string Title { get; set; }
        public long TotalCount { get; set; }
    }
}
