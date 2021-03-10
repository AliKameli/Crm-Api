using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Commands;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Sources.SourceConfig.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Sources
{
    public interface ISourceConfigRepository : IBaseRepository
    {
        Task<DataResponse<int>> CreateAsync(SourceConfigCreateCommand command);
        Task<DataResponse<IEnumerable<SourceConfigGetDto>>> GetAsync(SourceConfigDataTableQuery query);
        Task<DataResponse<SourceConfigModel>> GetByIdAsync(int id);
        Task<DataResponse<int>> UpdateAsync(SourceConfigUpdateCommand command);
    }
}
