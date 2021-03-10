using System;
using System.Collections.Generic;
using System.Text;

namespace CRCIS.Web.INoor.CRM.Domain.Users.Admin
{
    public class AdminModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DateTime AssignDateTime { get; set; }
        public string Mobile { get; set; }
        public string SerialNumber { get; set; }
        public string PasswordHash { get; set; }
    }
}
