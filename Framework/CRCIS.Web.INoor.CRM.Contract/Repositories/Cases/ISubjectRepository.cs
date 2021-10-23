using CRCIS.Web.INoor.CRM.Domain.Cases.Subject;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface ISubjectRepository
    {
        Task<DataResponse<int>> CreateAsync(SubjectCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataTableResponse<IEnumerable<SubjectGetDto>>> GetAsync(SubjectDataTableQuery query);
        Task<DataResponse<SubjectModel>> GetByIdAsync(int id);
        Task<DataTableResponse<IEnumerable<SubjectChildrenGetDto>>> GetChildrenAsync(SubjectChildrenDataTableQuery query);
        Task<DataResponse<IEnumerable<SubjectDropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<IEnumerable<SubjectDropDownListDto>>> GetSearchDropDownList(SubjectSearchDropDownQuery query);
        Task<DataResponse<int>> UpdateAsync(SubjectUpdateCommand command);
        Task<DataResponse<int>> UpdateSampleAsync();
    }
}
