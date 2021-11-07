using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig
{
   public class SourceConfigModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int SourceTypeId { get; set; }
        public int AnswerMethodId { get; set; }
        public string ConfigJson { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
