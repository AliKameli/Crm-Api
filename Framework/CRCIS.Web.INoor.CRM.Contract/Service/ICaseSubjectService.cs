using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Commands;
using CRCIS.Web.INoor.CRM.Domain.Cases.CaseSubject.Dtos;
using CRCIS.Web.INoor.CRM.Utility.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Contract.Service
{
    public interface ICaseSubjectService
    {
        Task<DataResponse<IEnumerable<CaseSubjectFullDto>>> AddSubjectAsync(UpdateCaseAddSubjectCommand command);
        Task<DataResponse<IEnumerable<CaseSubjectFullDto>>> RemoveSubjectAsync(UpdateCaseRemoveSubjectCommand command);
    }
}
