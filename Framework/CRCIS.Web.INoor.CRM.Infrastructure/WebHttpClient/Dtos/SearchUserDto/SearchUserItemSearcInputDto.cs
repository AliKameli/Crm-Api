using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.WebHttpClient.Dtos.SearchUserDto
{
    public class SearchUserItemSearcInputDto
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Mode { get; private set; } = "AND";
    }
}
