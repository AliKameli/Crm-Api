using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Sources
{
    public interface ISourceConfigRepository
    {
        Task<DataResponse<SourceConfigModel>> GetByIdAsync(int id);
        Task<DataResponse<IEnumerable<SourceConfigModel>>> GetBySourceTypesIdAsync(int sourceTypeId);
        Task<DataResponse<IEnumerable<SourceConfigModel>>> GetByAnswerMethodIdAsync(int answerMethodId);
    }
}
