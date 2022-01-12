using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Email.Commands
{
    public class MailboxImportCommand
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

        public string MoreData { get; private set; }
        public MailboxImportCommand(string title, string nameFamily, string email, string description,
            int sourceTypeId, int? productId, DateTime createDateTime, string toMailBox, List<string> attachments)
        {
            if (attachments is null )
            {
                attachments = new List<string>();
            }

            Title = title;
            NameFamily = nameFamily;
            Email = email;
            Description = description;
            SourceTypeId = sourceTypeId;
            ProductId = productId;
            ImportDateTime = DateTime.Now;
            CreateDateTime = createDateTime;

            var moreDataObject = new ImportCaseMoreDataObject(toMailBox ,attachments);
            MoreData = System.Text.Json.JsonSerializer.Serialize(moreDataObject);
        }

        public MailboxImportCommand()
        {

        }
    }
}
