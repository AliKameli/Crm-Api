using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Settings
{
    public interface IRabbitmqSettings
    {
        string VirtualHost { get; set; }
        string IssuQueueer { get; set; }

        string ExchangeFeedback { get; set; }
        string QueueFeedback { get; set; }

        string ExchangeNotification { get; set; }
        string QueueNotification { get; set; }

        string ExchangeCaseSujectUpdate { get; set; }
        string QueueCaseSujectUpdate { get; set; }

        string HostName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        int Port { get; set; }

    }
}
