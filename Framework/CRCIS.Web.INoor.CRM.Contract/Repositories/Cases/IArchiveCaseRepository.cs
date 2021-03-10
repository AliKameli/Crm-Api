using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Queris;
using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface IArchiveCaseRepository : IBaseRepository
    {
        Task<DataTableResponse<IEnumerable<ArchiveCaseGetDto>>> GetAsync(ArchiveCaseDataTableQuery query);
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command);
    }
}
