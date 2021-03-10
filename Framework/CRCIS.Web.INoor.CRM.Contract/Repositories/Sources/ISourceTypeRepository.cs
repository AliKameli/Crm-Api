using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceTypes.Queries;
using CRCIS.Web.INoor.CRM.Utility.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Sources
{
    public interface ISourceTypeRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(SourceTypeCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataTableResponse<IEnumerable<SourceTypeGetDto>>> GetAsync(SourceTypeDataTableQuery query);
        Task<DataResponse<SourceTypeModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<DropDownListDto>>> GetDropDownListAsync();
        Task<DataResponse<int>> UpdateAsync(SourceTypeUpdateCommand command);
    }
}
