using CRCIS.Web.INoor.CRM.Contract.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Settings
{
    public class RabbitmqSettings : IRabbitmqSettings
    {
        public string VirtualHost { get; set; }
        public string IssuQueueer { get; set; }

        public string ExchangeFeedback { get; set; }
        public string QueueFeedback { get; set; }

        public string ExchangeNotification { get; set; }
        public string QueueNotification { get; set; }

        public string ExchangeCaseSujectUpdate { get; set; }
        public string QueueCaseSujectUpdate { get; set; }

        public string ExchangeNoorlockSoftwareInserted { get; set; }
        public string QueueNoorlockSoftwareInserted { get; set; }

        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
