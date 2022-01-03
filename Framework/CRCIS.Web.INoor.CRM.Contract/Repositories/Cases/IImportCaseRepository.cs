using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Queries;
using CRCIS.Web.INoor.CRM.Domain.Cases.RabbitImport.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface IImportCaseRepository
    {
        Task<DataResponse<int>> CreateAsync(ImportCaseCreateCommand command, List<int> caseSubjectIds);
        Task<DataResponse<int>> CreateAndMoveToAdmin(ImportCaseCreateCommand command, int adminId, List<int> caseSubjectIds);
        Task<DataTableResponse<IEnumerable<ImportCaseGetFullDto>>> GetAsync(ImportCaseDataTableQuery query);
        Task<DataResponse<int>> AddCaseHistoryMoveCaseToCurrentAdminAsync(MoveCaseToCurrentAdminCardboardCommand command);
        Task<DataResponse<int>> AddCaseHistoryMoveCaseToArchiveAsync(MoveCaseToArchiveCommand command);
        Task MoveCaseToAdminAsync(long id);
        Task DeleteCaseAsync(long id);
        Task MoveCaseToArchiveAsync(long caseId);
    }
}
