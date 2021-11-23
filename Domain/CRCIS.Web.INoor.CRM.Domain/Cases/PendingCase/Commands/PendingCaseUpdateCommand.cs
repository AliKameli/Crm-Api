using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands
{
    public class PendingCaseUpdateCommand
    {
        public long Id { get;private set; }
        public string Title { get;private set; }
        public string NameFamily { get;private set; }
        public string Email { get;private set; }
        public string Description { get;private set; }
        public string Mobile { get;private set; }

        public int SourceTypeId { get;private set; }
        public int? ProductId { get;private set; }


        public string SubjectIds { get;private set; }

        public PendingCaseUpdateCommand(long id, string title, string nameFamily, string email, string description, string mobile, int sourceTypeId, int? productId, string subjectIds)
        {
            Id = id;
            Title = title;
            NameFamily = nameFamily;
            Email = email;
            Description = description;
            Mobile = mobile;
            SourceTypeId = sourceTypeId;
            ProductId = productId;
            SubjectIds = subjectIds;
        }
    }
}
