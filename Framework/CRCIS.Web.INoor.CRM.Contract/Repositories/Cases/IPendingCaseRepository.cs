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
    public interface IPendingCaseRepository
    {
        Task AddCaseHistoryMoveCaseToAdminAsync(int fromAdminId, int toAdminId, long caseId);
        Task<DataResponse<int>> AddCaseHistoryMoveCaseToArchive(int adminId, long caseId);
        Task DeleteCaseAsync(long caseId);
        Task<DataResponse<PendingCaseFullDto>> GetByIdAsync(long id);
        Task<DataTableResponse<IEnumerable<PendingCaseGetFullDto>>> GetForAdminAsync(AdminPendingCaseDataTableQuery query);
        Task MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardCommand command);
        Task MoveCaseToArchiveAsync(long caseId);
        Task<DataResponse<int>> UpdateCaseAsync(PendingCaseUpdateCommand command);
    }
}
