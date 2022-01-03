using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.UserReportSupports
{
    public class UserReportSupportConsumer : IConsumer<UserReportSupportValueDataEntered>
    {
        public Task Consume(ConsumeContext<UserReportSupportValueDataEntered> context)
        {
            throw new NotImplementedException();
        }
    }
}
