using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Commands
{
    public class SubjectUpdateCommand
    {
        public int Id { get;private set; }
        public string Title { get;private set; }
        public int? ParentId { get;private set; }
        public int? Priority { get; private set; }
        public bool IsActive { get;private set; }
        public SubjectUpdateCommand(int id, string title, int? parentId, bool isActive, int? priority)
        {
            Id = id;
            Title = title;
            ParentId = parentId;
            IsActive = isActive;
            Priority = priority;
        }
    }
}
