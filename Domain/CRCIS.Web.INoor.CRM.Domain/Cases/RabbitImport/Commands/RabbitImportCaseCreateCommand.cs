using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands
{
    public class RabbitImportCaseCreateCommand
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

        public string AppKeyHash { get; private set; }

        public RabbitImportCaseCreateCommand(string title, string nameFamily, string email, string description,
            int sourceTypeId, Guid? noorUserId, int? productId, int? manualImportAdminId, string mobile,
            string createDateTime, string pageTitle, string pageUrl, string toMailBox, string fileUrl, string fileType,
            string userLanguage, string ip, string browser, string userAgent, string platform,
            string os, string deviceScreenSize,
            string appKeyHash, string noorLockSk, long? noorLockSnId, string noorLockActivationCode, bool? noorLockTypeOfComment)
        {
            var now = DateTime.Now;
            DateTime? createdDateTime = null;
            try
            {
                createdDateTime = DateTime.ParseExact(createDateTime, "yyyymmddHHMMss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                createdDateTime = now;
            }

            Title = title;
            NameFamily = nameFamily;
            Email = email;
            Description = description;
            SourceTypeId = sourceTypeId;
            NoorUserId = noorUserId;
            ProductId = productId;
            ManualImportAdminId = manualImportAdminId;
            ImportDateTime = now;
            CreateDateTime = createdDateTime.GetValueOrDefault();
            Mobile = mobile;
            AppKeyHash = appKeyHash;

            var moreDataObject = new ImportCaseMoreDataObject(pageTitle, pageUrl, toMailBox, fileUrl, fileType,
                userLanguage, ip, browser, userAgent, platform, os, deviceScreenSize,
                noorLockSk, noorLockSnId, noorLockActivationCode, noorLockTypeOfComment
                );
            MoreData = System.Text.Json.JsonSerializer.Serialize(moreDataObject);
        }
    }

    public class ImportCaseMoreDataObject
    {
        public string PageTitle { get; private set; }
        public string PageUrl { get; private set; }

        [System.Text.Json.Serialization.JsonInclude]
        public string ToMailBox { get; private set; }
        public string FileUrl { get; private set; }
        public string FileType { get; private set; }
        public string UserLanguage { get; private set; }
        public string Ip { get; private set; }
        public string Browser { get; private set; }
        public string UserAgent { get; private set; }
        public string Platform { get; private set; }
        public string Os { get; private set; }
        public string DeviceScreenSize { get; private set; }

        public List <string> MailsAttachments { get; set; }
        //noorlock data
        public string NoorLockSk { get; private set; }
        public long? NoorLockSnId { get; private set; }
        public string NoorLockActivationCode { get; private set; }
        public bool? NoorLockTypeOfComment { get; private set; }


        public ImportCaseMoreDataObject(string pageTitle, string pageUrl, string toMailBox, string fileUrl, string fileType,
            string userLanguage, string ip, string browser, string userAgent, string platform, string os, string deviceScreenSize,
            string noorLockSk, long? noorLockSnId, string noorLockActivationCode, bool? noorLockTypeOfComment)
        {
            PageTitle = pageTitle;
            PageUrl = pageUrl;
            ToMailBox = toMailBox;
            FileUrl = fileUrl;
            FileType = fileType;
            UserLanguage = userLanguage;
            Ip = ip;
            Browser = browser;
            UserAgent = userAgent;
            Platform = platform;
            Os = os;
            DeviceScreenSize = deviceScreenSize;
            NoorLockSk = noorLockSk;
            NoorLockSnId = noorLockSnId;
            NoorLockActivationCode = noorLockActivationCode;
            NoorLockTypeOfComment = noorLockTypeOfComment;
        }
        public ImportCaseMoreDataObject(string toMailBox ,List<string> attachments)
        {
            ToMailBox = toMailBox;
            MailsAttachments = attachments;
        }
        public ImportCaseMoreDataObject()
        {

        }
    }
}
