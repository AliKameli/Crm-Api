using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Dtos
{
    public class RabbitImportCaseCreateDto
    {
        public Client Client { get; set; }
        public MessageInfo MessageInfo { get; set; }
        public User User { get; set; }
        public Device Device { get; set; }
        public AppKey AppKey { get; set; }
    }

    public class Client
    {
        public string ClientSecret { get; set; }
        public string PageTitle { get; set; }
        public string PageUrl { get; set; }
        public string ToMailBox { get; set; }
    }

    public class MessageInfo
    {
        public string NameFamily { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateDateTime { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
    }

    public class User
    {
        public string NoorUserId { get; set; }
        public string UserLanguage { get; set; }
        public string Ip { get; set; }
        public string NoorOrganizationId { get; set; }
        public string SessionId { get; set; }
    }

    public class Device
    {
        public string Browser { get; set; }
        public string UserAgent { get; set; }
        public string Platform { get; set; }
        public string Os { get; set; }
        public string DeviceScreenSize { get; set; }
    }

    public class AppKey
    {
        public string NoorLockSk { get; set; }
        public long? NoorLockSnId { get; set; }
        public string NoorLockActivationCode { get; set; }
        public bool NoorLockTypeOfComment { get; set; }
    }
}
