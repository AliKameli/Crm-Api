using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Users.Admin.Commands
{
    public class AdminCreateCommand
    {
        public DateTime AssignDateTime { get; private set; }
        public bool IsActive { get; private set; }
        public string SerialNumber { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Mobile { get; private set; }
        public Guid NoorPersonId { get; private set; }

        public AdminCreateCommand(string username, string password, string name, string family, string mobile, Guid noorPersonId)
        {
            Username = username;
            PasswordHash = password;
            IsActive = true;
            Name = name;
            Family = family;
            Mobile = mobile;
            AssignDateTime = DateTime.Now;
            SerialNumber = Guid.NewGuid().ToString();
            NoorPersonId = noorPersonId;
        }
    }
}