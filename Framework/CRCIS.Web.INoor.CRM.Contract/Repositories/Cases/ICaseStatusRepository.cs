using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface ICaseStatusRepository
    {
        Task<DataResponse<int>> CreateAsync(CaseStatusCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataResponse<IEnumerable<CaseStatusGetDto>>> GetAsync(CaseStatusDataTableQuery command);
        Task<DataResponse<CaseStatusModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<DropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<int>> UpdateAsync(CaseStatusUpdateCommand command);
    }
}
