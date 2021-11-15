using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Users.Admin.Dtos
{
    public class AdminGetDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public Guid NoorPersonId { get; set; }
        public int RowNumber { get; set; }
        public string Mobile { get; set; }
        public int TotalCount { get; set; }
    }
}
