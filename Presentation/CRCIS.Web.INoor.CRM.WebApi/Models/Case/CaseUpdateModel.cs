using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Models.Case
{
    public class CaseUpdateModel
    {
        public long Id { get;  set; }
        public string Title { get; set; }
        public string NameFamily { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Mobile { get; set; }

        public int SourceTypeId { get; set; }
        public string NoorUserId { get; set; }
        public int? ProductId { get; set; }

        public List<int> SubjectIds { get; set; }

     
    }
}
