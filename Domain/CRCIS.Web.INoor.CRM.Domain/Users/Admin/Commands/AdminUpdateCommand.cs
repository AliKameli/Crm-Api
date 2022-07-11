using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Users.Admin.Commands
{
    public class AdminUpdateCommand
    {
        public int Id { get; private set; }
        public bool IsActive { get; private set; }
    }
}