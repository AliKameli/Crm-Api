using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Permissions.RoleAdmin.Dtos
{
    public class RoleAdminGetDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AdminId { get; set; }
        public long TotalCount { get; set; }
    }
}
