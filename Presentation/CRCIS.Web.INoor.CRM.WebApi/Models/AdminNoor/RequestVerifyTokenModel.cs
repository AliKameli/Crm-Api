using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.AdminNoor
{
    public class RequestVerifyTokenModel
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public Dictionary<string, string> QueryString { get; set; }
    }
}
