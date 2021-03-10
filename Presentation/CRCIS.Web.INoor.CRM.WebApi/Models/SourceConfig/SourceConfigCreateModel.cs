using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.SourceConfig
{
    public class SourceConfigCreateModel
    {
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int SourceTypeId { get; set; }
        public string ConfigJson { get; set; }
    }
}
