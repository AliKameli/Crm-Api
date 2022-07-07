using CRCIS.Web.INoor.CRM.Domain.Alarms.WarningHistory.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.WarningHistory
{
    public interface IWarningHistoryRepository
    {
        Task<DataResponse<long>> CreateAsync(WarningHistoryCreateCommand command);
    }
}
