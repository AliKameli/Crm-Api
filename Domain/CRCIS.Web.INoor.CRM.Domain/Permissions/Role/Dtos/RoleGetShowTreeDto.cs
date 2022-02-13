using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.Role.Dtos
{
    public class RoleGetShowTreeDto
    {
        public int Key { get; set; }
        public string Title { get; set; }
        public int TotalCount { get; set; }
    }
}
