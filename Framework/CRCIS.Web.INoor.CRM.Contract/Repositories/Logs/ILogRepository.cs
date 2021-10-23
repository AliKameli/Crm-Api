using CRCIS.Web.INoor.CRM.Domain.Logs.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Logs
{
    public interface ILogRepository:IDisposable
    {
        DataResponse<int> Create(LogCreateCommand command);
        Task<DataResponse<int>> CreateAsync(LogCreateCommand command);
        Task<DataResponse<int>> CreateRangeAsync(IList<LogCreateCommand> commands, CancellationToken cancellationToken);
    }
}
