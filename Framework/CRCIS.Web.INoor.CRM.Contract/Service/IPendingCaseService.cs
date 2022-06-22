using CRCIS.Web.INoor.CRM.Domain.Cases.ImportCase.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.PendingCase.Commands;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface IPendingCaseService
    {
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardCommand command);
        Task<DataResponse<int>> MoveCaseToAdminAsync(MoveCaseToPartnerAdminCardboardMultiCommand commandMulti);
        Task<DataResponse<int>> MoveCaseToArchiveAsync(MoveCaseToArchiveCommand command);
        Task<DataResponse<int>> MoveCaseToArchiveAsync(MoveCaseToArchiveMultiCommand commandMulti);
        Task<DataResponse<int>> UpdateCaseAsync(PendingCaseUpdateCommand command);
    }
}
