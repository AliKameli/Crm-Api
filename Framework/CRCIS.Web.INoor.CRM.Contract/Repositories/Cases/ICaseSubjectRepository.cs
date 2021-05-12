using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Dtos;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Queries;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Repositories.Cases
{
    public interface ICaseSubjectRepository
    {
        Task<DataResponse<int>> CreateAsync(CaseSubjectCreateCommand command);
        Task<DataTableResponse<IEnumerable<CaseSubjectGetDto>>> GetAsync(CaseSubjectDataTableQuery query);
        Task<DataResponse<CaseSubjectModel>> GetByIdAsync(int id);
        Task<DataResponse<int>> UpdateAsync(CaseSubjectUpdateCommand command);
    }
}
