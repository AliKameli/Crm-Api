using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands
{
    public class ImportCaseCreateCommand
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public string NameFamily { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Description { get; private set; }
        public int SourceTypeId { get; private set; }
        public Guid? NoorUserId { get; private set; }
        public int? ProductId { get; private set; }
        public int? ManualImportAdminId { get; private set; }
        public DateTime ImportDateTime { get; private set; }
        public DateTime CreateDateTime { get; private set; }
        public ImportCaseCreateCommand(string title, string nameFamily, string email, string description,
            int sourceTypeId, Guid? noorUserId, int? productId, int? manualImportAdminId,string mobile)
        {
            Title = title;
            NameFamily = nameFamily;
            Email = email;
            Description = description;
            SourceTypeId = sourceTypeId;
            NoorUserId = noorUserId;
            ProductId = productId;
            ManualImportAdminId = manualImportAdminId;
            ImportDateTime = DateTime.Now;
            CreateDateTime = DateTime.Now;
            Mobile = mobile;
        }
    }
}
