using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface ICaseNewService
    {
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardCommand command);
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToCurrentAdminCardboardMultiCommand commandMulti);
        Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveCommand command);
        Task<DataResponse<int>> MoveCaseToArchive(MoveCaseToArchiveMultiCommand commandMulti);
    }
}
