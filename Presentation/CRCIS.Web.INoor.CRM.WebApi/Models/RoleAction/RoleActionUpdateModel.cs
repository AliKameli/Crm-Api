using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.RoleAction
{
    public class RoleActionUpdateModel
    {
        public int RoleId { get; set; }
        public List<int> ActionIds { get; set; }
    }
}
