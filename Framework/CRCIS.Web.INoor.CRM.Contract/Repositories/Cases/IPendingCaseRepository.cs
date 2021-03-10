using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface IPendingCaseRepository : IBaseRepository
    {
        Task<DataTableResponse<IEnumerable<PendingCaseGetDto>>> GetForAdminAsync(AdminPendingCaseDataTableQuery query);
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardCommand command);
        Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveCommand command);
    }
}
