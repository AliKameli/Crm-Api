using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Commands;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IWarningService
    {
        Task<DataResponse<long>> CreateAsync(WarningCreateCommand command);
        Task<DataResponse<long>> GetImportantWarningsDayCountAsync();
        Task<DataTableResponse<IEnumerable<WarningGetDto>>> GetWarningsAsync(WarningDataTableQuery query);
    }
}
