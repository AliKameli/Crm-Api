using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Queris;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface IArchiveCaseRepository 
    {
        Task AddCaseHistoryMoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command);
        Task DeleteAsync(long caseId);
        Task<DataTableResponse<IEnumerable<ArchiveCaseGetFullDto>>> GetAsync(ArchiveCaseDataTableQuery query);
        Task MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command);
    }
}
