using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Users.Admin.Dtos
{
    public class AdminByVerifyTokenDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Action { get; set; }
        public string JsonData { get; set; }

    }
}
