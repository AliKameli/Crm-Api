using CRCIS.Web.INoor.CRM.Domain.Cases.CaseStatus.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Dtoes;
using CRCIS.Web.INoor.CRM.Domain.Cases.OperationType.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface IOperationTypeRepository
    {
        Task<DataResponse<int>> CreateAsync(OperationTypeCreateCommand command);
        Task<DataResponse<int>> DeleteAsync(int id);
        Task<DataResponse<IEnumerable<OperationTypeGetDto>>> GetAsync(OperationTypeDataTableQuery query);
        Task<DataResponse<OperationTypeModel>> GetByIdAsync(int id);
        Task<DataResponse<int>> UpdateAsync(OperationTypeUpdateCommand command);
    }
}
