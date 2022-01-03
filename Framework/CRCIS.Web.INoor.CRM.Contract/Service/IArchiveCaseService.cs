using CRCIS.Web.INoor.CRM.Domain.Cases.ArchiveCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IArchiveCaseService
    {
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command);
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardMultiCommand commandMulti);
    }
}
