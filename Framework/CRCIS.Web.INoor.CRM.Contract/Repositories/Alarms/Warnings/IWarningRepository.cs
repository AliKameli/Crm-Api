using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Commands;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Alarms.Warnings.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Alarms.Warnings
{
    public interface IWarningRepository
    {
        Task<DataResponse<long>> CreateAsync(WarningCreateCommand command);
        Task<DataTableResponse<IEnumerable<WarningGetDto>>> GetAsync(WarningDataTableQuery query);
        Task<DataTableResponse<IEnumerable<WarningImportanstOnDayListDto>>> GetImportantWarningsDayAsync(ImportantWarningsDayQuery query);
        Task<DataResponse<long>> UpdateWarningAsVisitedAsync(WarningUpdateAsVistedCommand command);
    }
}
